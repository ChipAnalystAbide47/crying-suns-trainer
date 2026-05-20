#!/usr/bin/env python3
"""
Crying Suns Trainer - Main entry point.
Provides a CLI and GUI trainer for Crying Suns game memory manipulation.
"""
import argparse
import sys
import time
from src.memory import MemoryManager
from src.hacks import ResourceHack, FleetHack, ResearchHack
from src.logger import setup_logger

def main():
    parser = argparse.ArgumentParser(description="Crying Suns Trainer")
    parser.add_argument("--no-gui", action="store_true", help="Run in CLI mode without GUI")
    parser.add_argument("--resources", type=int, help="Set resources (e.g., 9999)")
    parser.add_argument("--fleet", action="store_true", help="Unlock all fleet slots")
    parser.add_argument("--research", action="store_true", help="Unlock all research")
    args = parser.parse_args()

    logger = setup_logger()
    logger.info("Starting Crying Suns Trainer...")

    if args.no_gui:
        # CLI mode
        manager = MemoryManager()
        if not manager.attach_process():
            logger.error("Could not attach to Crying Suns process.")
            sys.exit(1)
        if args.resources is not None:
            hack = ResourceHack(manager)
            hack.set_resources(args.resources)
            logger.info(f"Resources set to {args.resources}")
        if args.fleet:
            hack = FleetHack(manager)
            hack.unlock_all()
            logger.info("Fleet slots unlocked.")
        if args.research:
            hack = ResearchHack(manager)
            hack.unlock_all()
            logger.info("Research unlocked.")
        manager.detach()
    else:
        # GUI mode (placeholder for tkinter or PyQt)
        print("GUI mode not implemented yet. Use --no-gui for CLI.")
        sys.exit(1)

if __name__ == "__main__":
    main()
