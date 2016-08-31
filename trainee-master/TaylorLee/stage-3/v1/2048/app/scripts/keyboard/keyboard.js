'use strict';

angular
	.module('Keyboard', [])
	.service('KeyboardService', function($document) {

		var UP = 'up',
			RIGHT = 'right',
			DOWN = 'down',
			LEFT = 'left';

		var keyboardMap = {
			37: LEFT,
			38: UP,
			39: RIGHT,
			40: DOWN
		};

		// Initialize the keyboard event binding 
		// to start listening for keyboard events
		this.init = function() {
			var self = this;
			this.keyEventHandlers = [];
			$document.bind('keydown', function(event) {
				var key = keyboardMap[event.which];

				if (key) {
					// An interesting key was pressed
					event.preventDefault();
					self._handleKeyEvent(key, event);
				}
			});
		};

		this._handleKeyEvent = function(key, event) {
			var callbacks = this.keyEventHandlers;
			if (!callbacks) {
				return;
			}

			event.preventDefault();
			if (callbacks) {
				for (var x = 0; x < callbacks.length; x++) {
					var cb = callbacks[x];
					cb(key, event);
				}
			}
		};

		// Bind event handlers to get called
		// when an event is fired
		this.keyEventHandlers = [];
		this.on = function(cb) {
			this.keyEventHandlers.push(cb);
		};
	});