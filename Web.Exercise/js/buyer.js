
function C_ShowMessageBox(){
	$("#c_user").slideUp("fast");
	$("#c_message_box").slideToggle("fast");
}
function C_ShowUserBox(){
	$("#c_message_box").slideUp("fast");
	$("#c_user").slideToggle("fast");
}
$(".left .nav li").click(function () {
    if ($(this).children("ul").css("display") == "list-item") {

    }
    else {
        console.log("显示");
        $(this).children("ul").stop(false, false).slideToggle("2000");
    }
});
$(".nav li.active .title").click(function (event) {
    event.preventDefault();
});
//======================== 
//左侧菜单栏选择函数
//========================
function LeftMenuSelect(Index)
{
    var Count = 6;
    for (var i = 0; i < Count; i++)
    {
        $("#Menu").children("li").eq(i).removeClass("active");
    }
    if (Index >= 0 && Index < Count)
    {
        $("#Menu").children("li").eq(Index).addClass("active");
    }
}
