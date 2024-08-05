using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using BW2_Team6.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BW2_Team6.Services.Classes
{
	public class AnimalService : IAnimalService
	{
        private readonly DataContext _context;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(DataContext context, ILogger<AnimalService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Animal>> GetAllAsync()
        {
            try
            {
                return await _context.Animals
                    .AsNoTracking()
                    .Include(a => a.Owner)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero degli animali.");
                throw;
            }
        }

        public async Task<Animal> GetByIdAsync(int id)
        {
            try
            {
                var animal = await _context.Animals
                    .AsNoTracking()
                    .Include(a => a.Owner)
                    .FirstOrDefaultAsync(a => a.Id == id);
                if (animal == null)
                {
                    throw new KeyNotFoundException($"Animale con ID {id} non trovato.");
                }
                return animal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero dell'animale con ID {id}.");
                throw;
            }
        }

        public async Task<Animal> CreateAsync(Animal newAnimal)
        {
            if (newAnimal == null)
            {
                throw new ArgumentNullException(nameof(newAnimal));
            }

            try
            {
                await _context.Animals.AddAsync(newAnimal);
                await _context.SaveChangesAsync();
                return newAnimal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione dell'animale.");
                throw;
            }
        }

        public async Task<Animal> UpdateAsync(int id, Animal updatedAnimal)
        {
            if (updatedAnimal == null)
            {
                throw new ArgumentNullException(nameof(updatedAnimal));
            }

            try
            {
                var animal = await GetByIdAsync(id);
                animal.Name = updatedAnimal.Name;
                animal.Type = updatedAnimal.Type;
                animal.Fur = updatedAnimal.Fur;
                animal.DateBirth = updatedAnimal.DateBirth;
                animal.Microchip = updatedAnimal.Microchip;
                animal.Owner = updatedAnimal.Owner;

                _context.Animals.Update(animal);
                await _context.SaveChangesAsync();

                return animal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nell'aggiornamento dell'animale.");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var animal = await GetByIdAsync(id);
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella cancellazione dell'animale.");
                throw;
            }
        }
    }
}

