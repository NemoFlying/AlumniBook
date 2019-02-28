

window.onload = function () {
    var bodyH = $(window).height();
    $("body").css("height", bodyH + "px");
    $(".header").css("height", (bodyH * 0.1) + "px");
    $(".body").css("height", (bodyH * 0.9) + "px");
    $(".body").css({
        "height": (bodyH * 0.9) + "px", "margin-top": (bodyH * 0.1) + "px"
    });

    $(".offBtn").on("click", function () {
        window.location.href = "../home/logon";
    });
    $(".homeBtn").on("click", function () {
        window.location.href = "../home/home";
    });
    $(".MessageBoardBtn").on("click", function () {
        window.location.href = "../home/MessageBoard";
    });
    $(".PhotoAlbumBtn").on("click", function () {
        window.location.href = "../home/imglist";
    });
    $(".adminUser").on("click", function () {
        window.location.href = "../home/AdminUser";
    });
    $(".about").on("click", function () {
        window.location.href = "../home/about";
    });
    //PhotoAlbum
    function getBbs(data) {

    }
    $.ajax({
        dataType: "json",
        url: "../User/GetUserIndexInfo",
        data: {
        },
        success: function (data) {
            //console.log(data);

            $(".classHomeImg").append(`
                <img src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.UserInfo.QqId + `&s=140" alt="Alternate Text" />
            `);
            $(".userNames").text("" + data.UserInfo.UserName + "");

            //var formatTime1 = convertTime(this.Notice, "yyyy-MM-dd hh:mm:ss");//2015-07-11 14:12:29
            //$("#div1").text(formatTime1);
            //var formatTime2 = convertTime(dt, "yyyy年MM月dd日 hh时mm分ss秒");//2015年07月11日 14时12分29秒
            //$("#div2").text(formatTime2);
            $(".rightBottomL img").attr("src", "" + data.AlumCoverImgUrl + "");

            $(data.Notices).each(function () {
                console.log(this)
                $(".leftBottomDiv ul").append(`
                    <li>
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
                    </li>
                `);
            });

            $(data.Notices).each(function () {

                $(".leftBottomDiv ul").append(`
                    <li>
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
                    </li>
                `);
            });

            $(data.Classmate).each(function () {
                $(".StudentsInformation ul").append(`
                    <li>
                        <p>
                            <img class='ClassmateImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ this.QqId + `&s=40" alt="">
                            <span>`+ this.UserName + `</span>
                            <span>`+ this.QqId + `</span>
                        </p>
                    </li>

                `);
            });


            $(data.Bbs).each(function () {
                $(".MessageBoard ul").append(`
                    <li title='`+ this.Id + `'>
                        <p>
                            <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ this.QqId + `&s=40" alt="Alternate Text" />
                            <span>`+ this.createUserName + `</span>
                            <p class='Msg'>`+ this.Msg + `</p>
                            <span class='MsgTimes'>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</span>
                        </p>
                    </li>
                `);
            });

            $(".rightDiv").css({ "background": "url(" + data.BannerImgUrl +")"});
        }
        
    });
}
$(".addMessageBoardBtn").on("click", function () {
    var texts = $(".upMessageBoardDiv input").val();
    console.log(texts);
    $.ajax({
        dataType: "json",
        url: "../ClassInfo/AddClassBbs",
        data: {
            Msg:texts
        },
        success: function (reData) {       
            //console.log(this);
            if (reData.Status == true) {
                $(".upMessageBoardDiv input").val("");
                $(".MessageBoard li:last").append(`
                <li title = '`+ this.Id + `' >
                    <p>
                        <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ reData.Data.QqId + `&s=40" alt="Alternate Text" />
                        <span>`+ reData.Data.createUserName + `</span>
                        <p class='Msg'>`+ reData.Data.Msg + `</p>
                        <span class='MsgTimes'>`+ (new Date(parseInt(reData.Data.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</span>
                    </p>
                </li>
            `);
            } else {
                alert(reData.Msg + "添加失敗！");
            }


        }
    });
});
//$(function () {
//    var dt = '/Date(1436595149269)/';
//    var formatTime1 = convertTime(dt, "yyyy-MM-dd hh:mm:ss");//2015-07-11 14:12:29
//    console.log(formatTime1)
//    var formatTime2 = convertTime(dt, "yyyy年MM月dd日 hh时mm分ss秒");//2015年07月11日 14时12分29秒
//    $("#div2").text(formatTime2);
//})