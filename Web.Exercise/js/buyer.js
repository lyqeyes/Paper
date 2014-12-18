//========================
//初始化入口：统一界面自动函数初始化
//========================
function init(){
	//公司管理： 按钮组hover二级菜单
	dropMenu("#MemberTable .company_btn","#MemberTable div");
}
//========================
//  顶部控制面板  通用组件级别
//========================
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
//  二级菜单
//========================
function dropMenu(obj,tableobj){
	$(obj).each(function(){
		var theSpan = $(this);
		var theMenu = theSpan.next();
		var tarHeight = theMenu.height();
		theMenu.css({height:0,opacity:0});
		theSpan.hover(
			function(){
				theMenu.stop().show().animate({height:tarHeight,opacity:1},200);
			},
			function(){
				theMenu.stop().animate({height:0,opacity:0},200,function(){
					$(this).css({display:"none"});
				});
			}
		);
	});
	$(tableobj).hover(function(){
		$(this).stop().show().animate({height:tarHeight,opacity:1},400);
		},function(){
		$(this).stop().show().animate({height:0,opacity:1},400);
	});
}

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
