console.log("Welcome to Goldies!");

var button = $("findOutMoreButton");
button.on("click", function () {
    alert("*Take the user to a page with more info about Goldies.*");
});

var companyInfo = $(".goldies-lists li");
companyInfo.on("click", function () {
    console.log("You clicked on " + $(this).text());
});

var $loginToggle = $("#loginToggle");
var $popupForm = $(".popup-form");

$loginToggle.on("click", function () {
    $popupForm.fadeToggle(100);
});