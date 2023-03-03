using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Utilities;
using DAL.Models;
using Models.Place;
using Services;

namespace Test1
{
    public class PlaceServiceFake : IPlaceService
    {
        private readonly List<Place> _placeData;

        public PlaceServiceFake()
        {
            
            _placeData = new List<Place>()
            {
                new Place() { Id = 1, Name = "Orange Juice", Description= "Orange Tree" },
                new Place() { Id = 2, Name = "Diary Milk", Description="Cow"},
                new Place() { Id = 3, Name = "Frozen Pizza", Description="Uncle Mickey" }
            };
        }

        public IEnumerable<Place> GetAllItems()
        {
            return _placeData;
        }

        public Place Add(Place newItem)
        {
            newItem.Id =5;
            _placeData.Add(newItem);
            return newItem;
        }

        public Place GetById(long id)
        {
            return _placeData.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(long id)
        {
            var existing = _placeData.First(a => a.Id == id);
            _placeData.Remove(existing);
        }

        public Task<PlaceViewModel> Create(PlaceViewModel newItem, CancellationToken cancellationToken)
        {
            return Task.Run<PlaceViewModel>(() =>
            {
                newItem.Id = 5;
                Place model = new Place()
                {
                    Id = newItem.Id,
                    Description = newItem.Description,
                    Name = newItem.Name
                };
                _placeData.Add(model);
                return newItem;
            });
        }

        

        public Task<Place> Update(long id, PlaceViewModel cityViewModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            return Task.Run<bool>(() =>
            {
                var existing = _placeData.First(a => a.Id == id);
                return _placeData.Remove(existing);
            });
        }

        public Task<Place> Detail(long code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceAddress> CreatePlaceAddress(PlaceAddressViewModel placeViewModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceAddress> DetailPlaceAddress(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<PlaceAddress>> GetPlaceAddress(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePlaceAddress(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceAddress> UpdatePlaceAddress(long id, PlaceAddressViewModel viewModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<PlaceViewModel>> Get(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
