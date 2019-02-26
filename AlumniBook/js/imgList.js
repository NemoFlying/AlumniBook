

window.onload = function () {
    var bodyH = $(window).height();
    $("body").css("height", bodyH + "px");
    $(".header").css("height", (bodyH * 0.1) + "px");
    $(".body").css({
        "height": (bodyH * 0.9) + "px", "margin-top": (bodyH * 0.1) + "px"
    });

    $(".offBtn").on("click", function () {

        window.location.href = "../home/logon";
    });
    $.ajax({
        dataType: "json",
        url: "../User/GetUserIndexInfo",
        data: {
            //userName: $('#Username').val(),
            //password: $('#Password').val(),
        },
        success: function (data) {
            console.log(data);

            $(".classHomeImg").append(`
                <img src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.UserInfo.QqId + `&s=140" alt="Alternate Text" />
            `);
            $(".userNames").text("" + data.UserInfo.UserName + "");

            //var formatTime1 = convertTime(this.Notice, "yyyy-MM-dd hh:mm:ss");//2015-07-11 14:12:29
            //$("#div1").text(formatTime1);
            //var formatTime2 = convertTime(dt, "yyyy年MM月dd日 hh时mm分ss秒");//2015年07月11日 14时12分29秒
            //$("#div2").text(formatTime2);

            $(data.Notices).each(function () {
                $(".leftBottomDiv").append(`
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
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
                            <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.QqId + `&s=140" alt="Alternate Text" />
                            <span>`+ this.FromUser + `</span>
                            <span>`+ this.Notice + `</span>
                        </p>
                    </li>
                    <li title='`+ this.Id + `'>
                        <p>
                            <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.QqId + `&s=140" alt="Alternate Text" />
                            <span>`+ this.FromUser + `</span>
                            <span>`+ this.Notice + `</span>
                        </p>
                    </li>
                `);
            });

        }

    });
}

$(function () {
    var dt = '/Date(1436595149269)/';
    var formatTime1 = convertTime(dt, "yyyy-MM-dd hh:mm:ss");//2015-07-11 14:12:29
    console.log(formatTime1)
    var formatTime2 = convertTime(dt, "yyyy年MM月dd日 hh时mm分ss秒");//2015年07月11日 14时12分29秒
    $("#div2").text(formatTime2);
})