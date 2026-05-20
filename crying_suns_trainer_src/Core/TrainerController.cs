using System;

namespace CryingSunsTrainer.Core
{
    /// <summary>
    /// Coordinates cheat toggles and actions via the MemoryManager.
    /// Provides a clean interface for the console UI.
    /// </summary>
    public class TrainerController
    {
        private readonly MemoryManager _memory;
        private bool _infiniteHullActive;
        private bool _infiniteEnergyActive;

        public TrainerController(MemoryManager memory)
        {
            _memory = memory ?? throw new ArgumentNullException(nameof(memory));
            _infiniteHullActive = false;
            _infiniteEnergyActive = false;
        }

        /// <summary>
        /// Toggles infinite hull (ship health). When active, hull is set to 9999 each check.
        /// </summary>
        public void ToggleInfiniteHull()
        {
            _infiniteHullActive = !_infiniteHullActive;
            if (_infiniteHullActive)
            {
                _memory.WriteHull(9999);
                Console.WriteLine("Infinite Hull: ON");
            }
            else
            {
                Console.WriteLine("Infinite Hull: OFF");
            }
        }

        /// <summary>
        /// Toggles infinite energy. When active, energy is set to 500 each check.
        /// </summary>
        public void ToggleInfiniteEnergy()
        {
            _infiniteEnergyActive = !_infiniteEnergyActive;
            if (_infiniteEnergyActive)
            {
                _memory.WriteEnergy(500);
                Console.WriteLine("Infinite Energy: ON");
            }
            else
            {
                Console.WriteLine("Infinite Energy: OFF");
            }
        }

        /// <summary>
        /// Sets all resources (scrap, fuel, food) to maximum values.
        /// </summary>
        public void SetMaxResources()
        {
            _memory.WriteMaxResources();
            Console.WriteLine("Resources set to maximum (Scrap: 9999, Fuel: 999, Food: 999)");
        }

        /// <summary>
        /// Unlocks all technology nodes in the game.
        /// </summary>
        public void UnlockAllTechs()
        {
            _memory.WriteUnlockAllTechs();
            Console.WriteLine("All technologies unlocked.");
        }

        /// <summary>
        /// Returns whether infinite hull is currently active.
        /// </summary>
        public bool IsInfiniteHullActive => _infiniteHullActive;

        /// <summary>
        /// Returns whether infinite energy is currently active.
        /// </summary>
        public bool IsInfiniteEnergyActive => _infiniteEnergyActive;
    }
}
