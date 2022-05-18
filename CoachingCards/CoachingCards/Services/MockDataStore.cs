using CoachingCards.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachingCards.Services
{
    public class MockDataStore : IDataStore<Card>
    {
        readonly List<Card> items;

        public async Task<bool> AddAsync(Card item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Card item)
        {
            var oldItem = items.FirstOrDefault((Card arg) => arg.ID == item.ID);
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var oldItem = items.FirstOrDefault((Card arg) => arg.ID == id);
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Card> GetAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Card>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}