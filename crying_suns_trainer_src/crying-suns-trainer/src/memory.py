"""
Memory manager for reading/writing process memory.
Uses ctypes to interface with Windows API (kernel32).
"""
import ctypes
from ctypes import wintypes
import psutil

# Windows API constants
PROCESS_ALL_ACCESS = 0x1F0FFF

def _check_windows():
    """Ensure running on Windows."""
    import platform
    if platform.system() != "Windows":
        raise OSError("This trainer only works on Windows.")

class MemoryManager:
    """Handles process attachment and memory read/write operations."""

    def __init__(self):
        _check_windows()
        self._handle = None
        self._pid = None
        self._kernel32 = ctypes.WinDLL("kernel32", use_last_error=True)

    def attach_process(self, process_name: str = "CryingSuns.exe") -> bool:
        """Attach to the target process by name.

        Args:
            process_name: Name of the process (default: CryingSuns.exe)

        Returns:
            True if attached successfully, False otherwise.
        """
        for proc in psutil.process_iter(["pid", "name"]):
            if proc.info["name"] == process_name:
                self._pid = proc.info["pid"]
                self._handle = self._kernel32.OpenProcess(PROCESS_ALL_ACCESS, False, self._pid)
                if self._handle:
                    return True
        return False

    def read_int(self, address: int) -> int:
        """Read a 4-byte integer from the process memory.

        Args:
            address: Memory address to read from.

        Returns:
            Integer value read from memory.
        """
        buffer = ctypes.c_int(0)
        bytes_read = ctypes.c_size_t(0)
        self._kernel32.ReadProcessMemory(self._handle, address, ctypes.byref(buffer), ctypes.sizeof(buffer), ctypes.byref(bytes_read))
        return buffer.value

    def write_int(self, address: int, value: int) -> bool:
        """Write a 4-byte integer to the process memory.

        Args:
            address: Memory address to write to.
            value: Integer value to write.

        Returns:
            True if write succeeded, False otherwise.
        """
        buffer = ctypes.c_int(value)
        bytes_written = ctypes.c_size_t(0)
        result = self._kernel32.WriteProcessMemory(self._handle, address, ctypes.byref(buffer), ctypes.sizeof(buffer), ctypes.byref(bytes_written))
        return result != 0

    def detach(self):
        """Close the handle to the process."""
        if self._handle:
            self._kernel32.CloseHandle(self._handle)
            self._handle = None
            self._pid = None
