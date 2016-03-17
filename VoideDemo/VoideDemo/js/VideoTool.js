//加音量
function AddVolume() {
    var volume = myPlay.volume();
    if (volume >= 1) {
        alert("当前已是最大音量");
    } else {
        myPlay.volume(myPlay.volume() + 0.1);
    }
}
//减音量
function MinusVolume() {
    var volume = myPlay.volume();
    if (volume <= 0) {
        alert("当前已是最小音量");
    } else {
        myPlay.volume(myPlay.volume() - 0.1);
    }
}

//快近
function Forward() {
    myPlay.currentTime(myPlay.currentTime() + 1);
}

//快退
function Rewind() {
    myPlay.currentTime(myPlay.currentTime() - 1);
}

//播放暂停
function PlayOrPause() {
    if (myPlay.paused()) {
        $('#abvertising').hide();
        myPlay.play();
    } else {
        myPlay.pause();
    }
}


/*--右上  top: 10px  left: 468px close: top: 10px; left: 632px
左上  top: 10px; left: 10px  close: top: 10px; left: 174px
左下  top: 80px; left: 10px  close：top: 70px; left: 174px
中间  top: 50px; left: 230px close: top: 50px; left: 394px
右下  top: 80px; left: 468px close: top: 70px; left: 632px
-*/
//添加浮窗   -- 参数说明   开始的秒数   结束的秒数   图片路径    位置 （左上--leftTop、左下--leftDown、中间-center-、右上--reghtTop、右下--reghtDown）
function AddFloatingWindow(startSecond, endSecond, imgSrc, position) {

    if (imgSrc) {
        //设置图片路径
        $('#floatWindow').attr('src', imgSrc);
    } else {
        return '请设置给定图片地址';
    }
    if (position) {
        if (position == 'reghtTop') {
            $('#floatWindow').css('top', '10px').css('left', '468px');
            $('#close').css('top', '10px').css('left', '632px');
        } else if (position == 'leftDown') {
            $('#floatWindow').css('top', '80px').css('left', '10px');
            $('#close').css('top', '80px').css('left', '174px');
        } else if (position == 'center') {
            $('#floatWindow').css('top', '50px').css('left', '230px');
            $('#close').css('top', '50px').css('left', '394px');
        } else if (position == 'leftTop') {
            $('#floatWindow').css('top', '10px').css('left', '10px');
            $('#close').css('top', '10px').css('left', '174px');
        } else if (position == 'reghtDown') {
            $('#floatWindow').css('top', '80px').css('left', '468px');
            $('#close').css('top', '80px').css('left', '632px');
        } else {
            //默认为右下
            $('#floatWindow').css('top', '80px').css('left', '468px');
            $('#close').css('top', '80px').css('left', '632px');
        }
    } else {
        $('#floatWindow').css('top', '70px').css('left', '468px');
        $('#close').css('top', '70px').css('left', '632px');
    }
    if (startSecond && endSecond) {
        SetFWLoop(startSecond, endSecond);
        return 'ok';
    } else {
        return '请给定开始秒数与结束秒数';
    }


}
//设置浮窗时间
var ftime = 0;
function SetFWLoop(startSecond, endSecond) {

    var b = setInterval(function () {
        var cutime = myPlay.currentTime();
        cutime = Math.ceil(cutime);
        if ((cutime == startSecond || cutime == (startSecond + 1))) {
            ftime++;
            if (ftime >= endSecond) {
                $('#floatWindow').hide();
                $('#close').hide();
                ftime = 0;
                //clearInterval(b);
            } else {
                if (ftime == 1) {
                    $('#floatWindow').show();
                    $('#close').show();
                    flag = false;
                }
            }
        }
    }, 1000)
}


//广告为flash视频,需要给定flash视频的播放时长
function Addabvertising(startSecond, flashSec, flashTime) {
    if (!startSecond) {
        return '请给定广告开始的秒数';
    }
    if (!flashSec) {
        return '请给定广告视频的地址';
    }
    if (!flashTime) {
        return '请给定广告视频'
    }

    $('#abvertising').attr('src', flashSec);

    SetGGLoop(startSecond, flashTime);

    return 'ok';

}


function SetGGLoop(startSecond, endSecond) {
    var gtime = 0;
    var b = setInterval(function () {
        var cutime = myPlay.currentTime();
        cutime = Math.ceil(cutime);
        if ((myPlay.currentTime() == startSecond || cutime == (startSecond + 1))) {
            gtime++;
            if (gtime >= endSecond) {
                $('#abvertising').hide();
                $('#vedio').show();
                myPlay.play();
                //clearInterval(b);
                gtime = 0;
            } else {
                if (gtime == 1) {
                    myPlay.pause();
                    $('#vedio').hide();
                    $('#abvertising').show();
                    myPlay.pause();
                }
            }
        }
    }, 1000)
}