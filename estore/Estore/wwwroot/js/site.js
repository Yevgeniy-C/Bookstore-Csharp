// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var estore = estore || {};

estore.catalog = {
    addToCart: function (productid) {
        debugger;
        let id = document.querySelector('input[name="__RequestVerificationToken"]').value;
        const data = "__RequestVerificationToken=" + id + "&productid=" + productid;

        fetch("/cart/add/ajax",
        {
            method: "POST",
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
            body: data
        })
        .then(response => response.json())
        .then(result => {
            document.querySelector('.cart-widget .total').innerText = result.total;
        });
    },

    displayCartWidget: function () {
      
    },

    quicksearch: (field, resultdiv) => {
        let searchFor = document.querySelector(field).value;
        if (searchFor.length < 3) {
            document.querySelector(resultdiv).classList.add('d-none');
            return;
        }

        fetch('/search/quicksearch?searchfor=' + searchFor)
            .then(response => response.text())
            .then(data => {
                document.querySelector(resultdiv).classList.remove('d-none');
                document.querySelector(resultdiv).innerHTML = data;
            });
    },

    rate: (star) => {
        const starvalue = star.currentTarget.attributes['data-value'].value;
        document.getElementById('rating').value = starvalue;
        [1, 2, 3, 4, 5].forEach(e => {
            if (e <= starvalue) {
                document.querySelector('.star' + e).classList.add('star-selected');
            }
            else {
                document.querySelector('.star' + e).classList.remove('star-selected');
            }
        })
    }
}