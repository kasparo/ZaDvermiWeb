$(document).ready(function () {
    var html = $('html');
    html.data('previous-overflow', html.css('overflow'));
    html.css('overflow', 'hidden');

    $(".rope").click(function () {
        $(this).blur();
        if ($.cookie("curtain.state") == 'closed') {
            $(this).stop().animate({ top: '5px' }, { queue: false, duration: 350, easing: 'easeOutBounce' });

            $(".left-curtain").stop().animate({ width: '60px' }, 2000);
            $(".right-curtain").stop().animate(
                { width: '60px' },
                {
                    duration: 2000,
                    complete: function () {
                        html.css('overflow', html.data('previous-overflow'));
                        $(".navbar .container").fadeIn("fast");
                    }
                });
            
            $(".logo").fadeOut("slow");

            $.cookie("curtain.state", "opened");
        } else {
            $(".dropdown").removeClass("open");

            $(this).stop().animate({ top: '50px' }, { queue: false, duration: 350, easing: 'easeOutBounce' });
            $(".left-curtain").stop().animate({ width: '50%' }, 2000);
            $(".right-curtain").stop().animate(
                { width: '51%' },
                {
                    duration: 2000,
                    complete: function() {
                        html.css('overflow', 'hidden');
                        $(".logo").fadeIn("fast");
                    }
                });
                        
            $(".navbar .container").fadeOut("fast");
            
            $.cookie("curtain.state", "closed");
        }
        return false;
    });

    if ($.cookie("curtain.state")) {
        if ($.cookie("curtain.state") == 'opened')
            openCurtain();
    } else {
        $.cookie("curtain.state", "closed");
    }

});


function openCurtain() {
    $(".right-curtain").addClass("opened");
    $(".left-curtain").addClass("opened");
    
    $(".navbar .container").show();
}