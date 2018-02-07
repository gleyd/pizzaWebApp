var PizzaController = /** @class */ (function () {
    function PizzaController() {
        var _this = this;
        $(".row.pizzas").on("click", ".btn.btn-primary.detail", function (evt) {
            var id = $(evt.target).attr("data-id");
            _this.getDetails(id);
        });
    }
    PizzaController.prototype.getDetails = function (id) {
        $.get("/Pizza/Detail/" + id)
            .then(function (data) {
            $(".container-modal").html(data);
            $("#modalPizza").modal('show');
        });
    };
    return PizzaController;
}());
var pizzaCtrl = new PizzaController();
//plus facile a maintenir et confortable plus fluide
// compatible a plusieur navigateur grace au transpileur 
//# sourceMappingURL=pizza-controller.js.map