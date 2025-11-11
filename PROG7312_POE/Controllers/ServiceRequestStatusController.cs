using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;
using PROG7312_POE.Models.DataStructures;

namespace PROG7312_POE.Controllers
{
    public class ServiceRequestStatusController : Controller
    {
        private readonly InMemoryRepository _repo;

        public ServiceRequestStatusController(InMemoryRepository repo)
        {
            _repo = repo;

            _repo.ClearSearchHistory();
        }

        [HttpGet]
        public IActionResult Index(string? searchRef)
        {
            var requests = _repo.GetServiceRequests().ToList();
            var bst = new BinarySearchTree<string>();
            var heap = new MinHeap<int>();
            var graph = new Graph();

            foreach (var r in requests)
            {
                bst.Insert(r.ReferenceCode);
                heap.Add(r.Priority);
                if (r.Status == "In Progress") graph.AddEdge("Pending", "In Progress");
                else if (r.Status == "Completed") graph.AddEdge("In Progress", "Completed");
            }

            var searchResult = !string.IsNullOrEmpty(searchRef)
                ? _repo.FindRequest(searchRef)
                : null;

            var vm = new ServiceRequestStatusViewModel
            {
                Requests = requests,
                FoundRequest = searchResult,
                OrderedRefs = bst.InOrder().ToList(),
                HeapPriorities = heap.ToList(),
                GraphPath = graph.BFS("Pending")
            };

            return View(vm);
        }
    }
}
