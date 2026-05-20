"""
Logging setup for the trainer.
"""
import logging
import sys

def setup_logger(name: str = "CryingSunsTrainer", level: int = logging.INFO) -> logging.Logger:
    """Configure and return a logger with console output.

    Args:
        name: Logger name.
        level: Logging level (default: INFO).

    Returns:
        Configured logger instance.
    """
    logger = logging.getLogger(name)
    logger.setLevel(level)

    if not logger.handlers:
        handler = logging.StreamHandler(sys.stdout)
        handler.setLevel(level)
        formatter = logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s")
        handler.setFormatter(formatter)
        logger.addHandler(handler)

    return logger
