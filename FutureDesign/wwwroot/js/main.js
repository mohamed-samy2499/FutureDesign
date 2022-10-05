$("#toggle-btn").click(function () {
    let option_w = $(".options-bar").innerWidth();
    console.log(option_w);
    if ($(".options-bar").css("left") == "0px") {
        $('#now-playing').addClass('awl-li').removeClass('awl1-li');
        $('#popular').addClass('tany-li').removeClass('tany1-li');
        $('#top-rated').addClass('talt-li').removeClass('talt1-li');
        $(".options-bar").animate({ left: `-${option_w}px` }, 1000);
        $(".logo-bar").animate({ left: `0px` }, 1000, function () {

            $("#toggle-btn").empty().append("<i class='fas fa-phone'style='color:#f25454;'></i>");

        });

    }
    else {
        $(".logo-bar").animate({ left: `50px` }, 1000, function () {
            $('#now-playing').addClass('awl1-li').removeClass('awl-li');
            $('#popular').addClass('tany1-li').removeClass('tany-li');
            $('#top-rated').addClass('talt1-li').removeClass('talt-li');
        });

        $(".options-bar").animate({ left: `0px` }, 1000, function () {

            $("#toggle-btn").empty().append("<i class='fas fa-times'style='color:#f25454;'></i>");
        });

    }


})

