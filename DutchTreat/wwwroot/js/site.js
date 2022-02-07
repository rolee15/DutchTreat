// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(() => {
    console.log("Hello Pluralsight");

    var theForm = $("#theForm");
    theForm.hide();

    var button = $("#buyButton");
    button.on("click", () => {
        console.log("Buying Item");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", () => {
        console.log("You clicked on " + $(this).text());
    });

    var loginToggle = $("#loginToggle");
    var popupForm = $(".popup-form");

    loginToggle.on("click", () => {
        popupForm.fadeToggle(500);
    });
});