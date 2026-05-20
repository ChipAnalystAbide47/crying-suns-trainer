"""
Hack modules for Crying Suns.
Each class implements a specific cheat functionality.
"""
from .memory import MemoryManager

class ResourceHack:
    """Manipulate in-game resources (credits, materials, etc.)."""

    def __init__(self, manager: MemoryManager):
        self._manager = manager
        # Example offset (placeholder - real offsets would be found via memory scanning)
        self._resource_base = 0x00A1B2C0

    def set_resources(self, amount: int) -> bool:
        """Set resources to a specific amount.

        Args:
            amount: Desired resource value.

        Returns:
            True if successful.
        """
        # Write to multiple offsets (credits, materials, etc.)
        success = True
        for offset in [0x00, 0x04, 0x08]:
            addr = self._resource_base + offset
            if not self._manager.write_int(addr, amount):
                success = False
        return success

class FleetHack:
    """Unlock fleet slots and modify ship stats."""

    def __init__(self, manager: MemoryManager):
        self._manager = manager
        self._fleet_base = 0x00B2C3D0

    def unlock_all(self) -> bool:
        """Unlock all fleet slots.

        Returns:
            True if successful.
        """
        # Write 1 to unlock flags
        success = True
        for i in range(6):
            addr = self._fleet_base + (i * 0x10)
            if not self._manager.write_int(addr, 1):
                success = False
        return success

class ResearchHack:
    """Unlock all research nodes."""

    def __init__(self, manager: MemoryManager):
        self._manager = manager
        self._research_base = 0x00C3D4E0

    def unlock_all(self) -> bool:
        """Set all research nodes as unlocked.

        Returns:
            True if successful.
        """
        success = True
        for offset in range(0, 0x100, 4):
            addr = self._research_base + offset
            if not self._manager.write_int(addr, 1):
                success = False
        return success
