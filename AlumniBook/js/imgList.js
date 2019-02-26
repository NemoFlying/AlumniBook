

window.onload = function () {
    var bodyH = $(window).height();
    $("body").css("height", bodyH + "px");
    $(".header").css("height", (bodyH * 0.1) + "px");
    $(".body").css({
        "height": (bodyH * 0.9) + "px", "margin-top": (bodyH * 0.1) + "px"
    });
    var imgW = $(".imglist li div").width();
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

    function getImg(data) {
        $(data).each(function () {
            $(".imglist").append(`
                    <li title='`+ this.Id + `'>
                        <div>
                            <img src="`+ this.PhotoUrl + `" alt="Alternate Text" />
                        </div>
                        <div class="imgMsg">
                            <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p><span>` + this.CreateUser + `</span><span>上傳至</span><span>《相冊》</span>
                            <button type="button" class='delImgBtn'></button>
                        </div>
                    </li>
                `);
        });
    };
    $.ajax({
        dataType: "json",
        url: "../User/GetUserIndexInfo",
        data: {
            //userName: $('#Username').val(),
            //password: $('#Password').val(),
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

            $(data.Notices).each(function () {
                $(".leftBottomDiv").append(`
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
                `);
            });

        }

    });
    $.ajax({
        dataType: "json",
        url: "../ClassInfo/GetCurrentClassAlbums",
        data: {
            //userName: $('#Username').val(),
            //password: $('#Password').val(),
        },
        success: function (reData) {
            //console.log(data);
            $(".imglist").empty();
            getImg(reData);
            $(".delImgBtn").click(function () {
                var ImgId = $(this).parents("li").attr("title");
                $.ajax({
                    dataType: "json",
                    url: "../ClassInfo/DeleteClassAlbums",
                    data: {
                        albumsId: ImgId
                    },
                    success: function (reData) {
                        console.log(reData);
                        //$(".imglist").empty();
                        //getImg(reData);
                    }
                });
            });
        }
    });

}

//$(function () {
//    var dt = '/Date(1436595149269)/';
//    var formatTime1 = convertTime(dt, "yyyy-MM-dd hh:mm:ss");//2015-07-11 14:12:29
//    console.log(formatTime1)
//    var formatTime2 = convertTime(dt, "yyyy年MM月dd日 hh时mm分ss秒");//2015年07月11日 14时12分29秒
//    $("#div2").text(formatTime2);
//})