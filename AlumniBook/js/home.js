

window.onload = function () {
    var bodyH = $(window).height();
    $("body").css("height", bodyH + "px");
    $(".header").css("height", (bodyH * 0.1) + "px");
    $(".body").css("height", (bodyH * 0.9) + "px");

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

                //对外暴露的函数,替换掉/Date( )/
                function convertTime(jsonTime, format) {
                    var date = new Date(parseInt(jsonTime.replace("/Date(", "").replace(")/", ""), 10));
                    var formatDate = date.format(format);
                    return formatDate;
                }

                //先扩展一下javascript的Date类型,增加一个函数,用于返回我们想要的 yyyy-MM-dd HH:mm:ss 这种时间格式
                Date.prototype.format = function (format) {
                    var date = {
                        "M+": this.getMonth() + 1,
                        "d+": this.getDate(),
                        "h+": this.getHours(),
                        "m+": this.getMinutes(),
                        "s+": this.getSeconds(),
                        "q+": Math.floor((this.getMonth() + 3) / 3),
                        "S+": this.getMilliseconds()
                    };

                    if (/(y+)/i.test(format)) {
                        format = format.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length));
                    }

                    for (var k in date) {
                        if (new RegExp("(" + k + ")").test(format)) {
                            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? date[k] : ("00" + date[k]).substr(("" + date[k]).length));
                        }
                    }

                    return format;
                }
                var times = this.NoticDate;
                    var formatTime1 = convertTime(times, "yyyy-MM-dd hh:mm:ss");
                    $(".leftBottomDiv").append(`
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ formatTime1 + `</p>
                `);
                });

            $(data.Classmate).each(function () {
                $(".StudentsInformation ul").append(`
                    <li>
                        <p>
                            <img class='ClassmateImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ this.QqId + `&s=40" alt="">
                            <span>`+ this.UserName +`</span>
                            <span>`+ this.QqId+`</span>
                        </p>
                    </li>

                `);
            });

            $(data.Bbs).each(function () {
                $(".MessageBoard ul").append(`
                    <li title='`+ this.Id+`'>
                        <p>
                            <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.QqId + `&s=140" alt="Alternate Text" />
                            <span>`+ this.FromUser+`</span>
                            <span>`+ this.Notice+`</span>
                        </p>
                    </li>
                    <li title='`+ this.Id + `'>
                        <p>
                            <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.QqId + `&s=140" alt="Alternate Text" />
                            <span>`+ this.FromUser + `</span>
                            <span>`+ this.Notice +`</span>
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