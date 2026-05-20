"""
Unit tests for hack modules.
Uses mocking to avoid actual process attachment.
"""
import unittest
from unittest.mock import MagicMock, patch
from src.hacks import ResourceHack, FleetHack, ResearchHack
from src.memory import MemoryManager

class TestHacks(unittest.TestCase):
    """Test cases for hack classes."""

    def setUp(self):
        """Set up mock memory manager."""
        self.mock_manager = MagicMock(spec=MemoryManager)
        self.mock_manager.write_int.return_value = True

    def test_resource_hack_set(self):
        """Test setting resources."""
        hack = ResourceHack(self.mock_manager)
        result = hack.set_resources(9999)
        self.assertTrue(result)
        # Should have called write_int three times
        self.assertEqual(self.mock_manager.write_int.call_count, 3)

    def test_fleet_hack_unlock_all(self):
        """Test unlocking all fleet slots."""
        hack = FleetHack(self.mock_manager)
        result = hack.unlock_all()
        self.assertTrue(result)
        # Should have called write_int six times
        self.assertEqual(self.mock_manager.write_int.call_count, 6)

    def test_research_hack_unlock_all(self):
        """Test unlocking all research."""
        hack = ResearchHack(self.mock_manager)
        result = hack.unlock_all()
        self.assertTrue(result)
        # Should have called write_int 64 times (0x100 / 4)
        self.assertEqual(self.mock_manager.write_int.call_count, 64)

    def test_resource_hack_failure(self):
        """Test resource hack when write fails."""
        self.mock_manager.write_int.return_value = False
        hack = ResourceHack(self.mock_manager)
        result = hack.set_resources(100)
        self.assertFalse(result)

if __name__ == "__main__":
    unittest.main()
