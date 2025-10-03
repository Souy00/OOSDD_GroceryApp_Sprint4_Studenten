
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository = groceryListItemsRepository;
            _groceryListRepository = groceryListRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            if (productId == null) return new List<BoughtProducts>();
            var result = new List<BoughtProducts>();

            var clients = _clientRepository.GetAll();
            foreach (var client in clients)
            {
                foreach (var list in client.GroceryLists)
                {
                    var items = _groceryListItemsRepository.GetAll()
                        .Where(i => i.GroceryListId == list.Id && i.ProductId == productId);

                    foreach (var item in items)
                    {
                        item.Product = _productRepository.Get(item.ProductId) ?? new Product(0, " ", 0);

                        result.Add(new BoughtProducts(client, list, item.Product));
                    }

                }
            }
            return result;
        }
    }

}   