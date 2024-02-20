$(document).ready(function () {
    $(".yorum-ekle-btn").on("click", function () {
        var kitapId = $(this).data("kitapid");

        $(".yorum-section").not("[data-kitapid=" + kitapId + "]").hide(); // tüm yorum bölümlerini gizliyorum

        $(".yorum-section[data-kitapid=" + kitapId + "]").toggle(); // gizli elementlere tıklandığında görünür hale gelmelerini sağlıyorum.
        // tıklanan bölümün section unu açıyorum
    });
    $(".yorum-submit-btn").on("click", function () { // yorum ekle butonuna basınca kitapId ve yorumIcerigi değerlerini alıyorum
        var kitapId = $(this).data("kitapid");
        var yorumIcerigi = $("#yorumIcerigi_" + kitapId).val();

        $.ajax({
            url: "/Yorum/YorumEkle",
            type: "POST",
            data: { kitapId: kitapId, yorumIcerigi: yorumIcerigi },
            success: function (result) {
                if (result.success) {
                    alert(result.message);
                    location.reload(true);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                alert("Beklenmeyen bir hata oluştu!");
            }
        });
    });
});


$(document).ready(function () {
    $(".cevap-ver-btn").on("click", function () {
        var yorumId = $(this).data("yorumid");

        $(".cevap-form").not("[data-yorumid=" + yorumId + "]").hide();

        $(".cevap-form[data-yorumid=" + yorumId + "]").toggle();
    });

    $(".cevap-submit-btn").on("click", function () {
        var yorumId = $(this).data("yorumid");
        var cevapIcerigi = $("#cevapIcerigi_" + yorumId).val();

        $.ajax({
            url: "/Yorum/CevapEkle",
            type: "POST",
            data: { yorumId: yorumId, cevapIcerigi: cevapIcerigi },
            success: function (result) {
                if (result.success) {
                    alert(result.message);
                    location.reload(true);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                alert("Beklenmeyen bir hata oluştu!");
            }
        });
    });
});
