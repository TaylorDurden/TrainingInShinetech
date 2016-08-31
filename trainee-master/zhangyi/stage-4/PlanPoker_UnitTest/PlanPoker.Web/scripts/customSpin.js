$.fn.startSpin = function (size) {
    this.each(function () {
        var $this = $(this),
            data = $this.data();

        if (data.spinner) {
            data.spinner.stop();
            delete data.spinner;
        }

        var length = 7;
        var lines = 13;
        var width = 4;
        var radius = 10;

        if (size == 1) {
            lines = 10;
            width = 2;
            radius = 7;
            length = 4;
        }
        if (size == 2) {
            lines = 13;
            width = 4;
            radius = 10;
            length = 7;
        }

        var opts = {
            lines: lines, // The number of lines to draw
            length: length, // The length of each line
            width: width, // The line thickness
            radius: radius, // The radius of the inner circle
            rotate: 0, // The rotation offset
            color: '#000', // #rgb or #rrggbb
            speed: 1, // Rounds per second
            trail: 60, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        };
        data.spinner = new Spinner($.extend({ color: $this.css('color') }, opts)).spin(this);
        $this.attr('disabled', "disabled");
    });
    return this;
};

$.fn.stopSpin = function () {
    this.each(function () {
        var $this = $(this),
            data = $this.data();
        $this.removeAttr('disabled');
        if (data.spinner) {
            data.spinner.stop();
            delete data.spinner;

        }
    });
    return this;
};