'use strict';

angular
.module('Game', [])
.service('GameManager', function(GridService){
	// Create a new game
	this.newGame = function() {
		GridService.buildEmptyGameBoard();
		GridService.buildStartingPosition();
		this.reinit();
	};

	// Reset game state
	this.reinit = function() {
		this.gameOver = false;
		this.win = false;
		this.currentScore = 0;
		this.highScore = 0; // We'll come back to this
	};

	// Handle the move action
	this.move = function(key) {
		var self = this; // Hold a reference to the GameManager, for later

		// define move here
		if (self.win) {
			return false;
		}

		var positions = GridService.traversalDirections(key);

		positions.x.forEach(function(x) {
			positions.y.forEach(function(y) {
				// For every position
				// save the tile's original position
				var originalPosition = {x: x, y: y};
				var tile = GridService.getCellAt(originalPosition);

				if (tile) {
					// if we have a tile here
					var cell = GridService.calculateNextPosition(tile, key);
						next = cell.next;
					// ...
					if (next && 
						next.value === tile.value &&
						!next.merged) {
						// Handle merged
					} else {
						GridService.moveTile(tile, cell.newPosition);
					}
					// ...
				}
			});
		});
	};
	// Update the score
	this.updateScore = function(newScore) {};
	// Are there moves left?
	this.movesAvailable = function() {
		return GridService.anyCellsAvailable() ||
				GridService.tileMatchesAvailable();
	};
});