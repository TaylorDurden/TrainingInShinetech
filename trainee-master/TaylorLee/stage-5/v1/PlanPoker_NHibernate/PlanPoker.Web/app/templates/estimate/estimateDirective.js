angular
    .module('EstimateModule')
    .directive('estimatepoker', function () {
    return {
        restrict: 'EA',
        replace: true,
        transclude: true,
        template:
            '<div class="col-xs-4 col-md-2 plan-poker">' +
            '<div class="thumbnail">' +
            '<img ng-src="/app/imgs/{{flipped ? estimate.SelectedPoker:planpoker}}.jpg" id="card" alt="pic">' +
            '<div class="caption user-info" >' +
            '<img style="width: 40px; height: 40px;" ng-src="{{estimate.UserImage?estimate.UserImage:defaultImageUrl}}" alt="pic">' +
            '<label class="pull-right">{{estimate.UserName}}</label>' +
            '</div></div></div>',
        link: function (scope) {
            scope.planpoker = 'planpoker';
            scope.defaultImageUrl = '/app/imgs\\user.png';
        }
    }
});