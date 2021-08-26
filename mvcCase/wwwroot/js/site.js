// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#doLoginBtn").click(function(){
    $.post("/Admin/doLogin",
        {
            username: $("#inputUsername").val(),
            password: $("#inputPassword").val()
        },
        function(data){
        alert("Data: " + data.result.message);
    });
});

function displayDate() {
    $.post("/Admin/doLogin", function(data, status){
        alert("Data: " + data);
    });
}
function displayDate2() {
    document.getElementById("demo2").innerHTML = "第三种script方式显示时间是："
        + Date();
}