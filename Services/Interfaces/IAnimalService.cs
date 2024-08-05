using System;
using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
	public interface IAnimalService
	{
        public async Task<List<Animal>> GetAll();
        public async Task<Animal> GetById(int id);
        public async Task<Animal> Create(Animal newAnimal);
        public async Task<Animal> Update(int id, Animal updatedAnimal);
        public async Task<Animal> Delete(int id);
    }
}

