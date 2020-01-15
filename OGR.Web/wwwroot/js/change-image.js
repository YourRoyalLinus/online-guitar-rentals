//DETAIL VIEW

//Change Main Image to Origional Image (Detail)
$(document).ready(function () {
    $(".content .imageDiv div.altImages #altImg1").click(function () {
        $(".content div.imageDiv #mainImage").attr("src", "" + localStorage.getItem("imageUrl") + "")
        $(".content .imageDiv div.altImages img").css("border", "2px solid grey")
        $(".content .imageDiv div.altImages #altImg1").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 1 (Detail)
$(document).ready(function () {
    $(".content .imageDiv div.altImages #altImg2").click(function () {
        $(".content div.imageDiv #mainImage").attr("src", "" + localStorage.getItem("altImageUrl1") + "")
        $(".content .imageDiv div.altImages img").css("border", "2px solid grey")
        $(".content .imageDiv div.altImages #altImg2").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 2 (Detail)
$(document).ready(function () {
    $(".content .imageDiv div.altImages #altImg3").click(function () {
        $(".content div.imageDiv #mainImage").attr("src", "" + localStorage.getItem("altImageUrl2") + "")
        $(".content .imageDiv div.altImages img").css("border", "2px solid grey")
        $(".content .imageDiv div.altImages #altImg3").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 3 (Detail)
$(document).ready(function () {
    $(".content .imageDiv div.altImages #altImg4").click(function () {
        $(".content div.imageDiv #mainImage").attr("src", "" + localStorage.getItem("altImageUrl3") + "")
        $(".content .imageDiv div.altImages img").css("border", "2px solid grey")
        $(".content .imageDiv div.altImages #altImg4").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 4 (Detail)
$(document).ready(function () {
    $(".content .imageDiv div.altImages #altImg5").click(function () {
        $(".content div.imageDiv #mainImage").attr("src", "" + localStorage.getItem("altImageUrl4") + "")
        $(".content .imageDiv div.altImages img").css("border", "2px solid grey")
        $(".content .imageDiv div.altImages #altImg5").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 5 (Detail)
$(document).ready(function () {
    $(".content .imageDiv div.altImages #altImg6").click(function () {
        $(".content div.imageDiv #mainImage").attr("src", "" + localStorage.getItem("altImageUrl5") + "")
        $(".content .imageDiv div.altImages img").css("border", "2px solid grey")
        $(".content .imageDiv div.altImages #altImg6").css("border", "solid 2px orange")
    });
});

// RENT/RETURN/HOLD VIEWS 

//Change Main Image to Orignal Image (R/R/H)
$(document).ready(function () {
    $(".contentBox .subBox div.altImages #altImg1").click(function () {
        $(".contentBox .col-md-6 div #mainImage").attr("src", "" + localStorage.getItem("imageUrl") + "")
        $(".contentBox .subBox div.altImages img").css("border", "2px solid grey")
        $(".contentBox .subBox div.altImages #altImg1").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 1 (R/R/H)
$(document).ready(function () {
    $(".contentBox .subBox div.altImages #altImg2").click(function () {
        $(".contentBox .col-md-6 div #mainImage").attr("src", "" + localStorage.getItem("altImageUrl1") + "")
        $(".contentBox .subBox div.altImages img").css("border", "2px solid grey")
        $(".contentBox .subBox div.altImages #altImg2").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 2 (R/R/H)
$(document).ready(function () {
    $(".contentBox .subBox div.altImages #altImg3").click(function () {
        $(".contentBox .col-md-6 div #mainImage").attr("src", "" + localStorage.getItem("altImageUrl2") + "")
        $(".contentBox .subBox div.altImages img").css("border", "2px solid grey")
        $(".contentBox .subBox div.altImages #altImg3").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 3 (R/R/H)
$(document).ready(function () {
    $(".contentBox .subBox div.altImages #altImg4").click(function () {
        $(".contentBox .col-md-6 div #mainImage").attr("src", "" + localStorage.getItem("altImageUrl3") + "")
        $(".contentBox .subBox div.altImages img").css("border", "2px solid grey")
        $(".contentBox .subBox div.altImages #altImg4").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 4 (R/R/H)
$(document).ready(function () {
    $(".contentBox .subBox div.altImages #altImg5").click(function () {
        $(".contentBox .col-md-6 div #mainImage").attr("src", "" + localStorage.getItem("altImageUrl4") + "")
        $(".contentBox .subBox div.altImages img").css("border", "2px solid grey")
        $(".contentBox .subBox div.altImages #altImg5").css("border", "solid 2px orange")
    });
});

//Change Main Image to Alt Image 5 (R/R/H)
$(document).ready(function () {
    $(".contentBox .subBox div.altImages #altImg6").click(function () {
        $(".contentBox .col-md-6 div #mainImage ").attr("src", "" + localStorage.getItem("altImageUrl5") + "")
        $(".contentBox .subBox div.altImages img").css("border", "2px solid grey")
        $(".contentBox .subBox div.altImages #altImg6").css("border", "solid 2px orange")
    });
});