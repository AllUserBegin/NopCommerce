


var quickViewApi = function() {
};


quickViewApi.prototype.viewProductDetails = function(options) {
    var config = $.extend({
        data: { },
        success: function() {
        },
        error: function() {
        }
    }, options);

    $.apiCall({
        type: 'POST',
        data: config.data,
        url: '/bs_product_details',
        success: function(reponse) {

            $("#quick-view-modal").html(reponse.html);
            console.log(reponse);
            $(document).trigger("hide-ajax-loading");
            $("#quick-view-loading-modal").modal("hide");
            $("#quick-view-product-details-modal").modal("show");

        }
    });

};






var api = new quickViewApi();