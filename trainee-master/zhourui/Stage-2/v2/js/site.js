//$(document).ready(function () {
//    $(":input").on("blur", function () {
//        var $parent = $(this).parent();
//        $parent.find(".has-error").remove();
//        $parent.find(".has-warning").remove();
//
//        var regName = /^[\da-z]+$/i;
//        var regEmail = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
//        var regPassword = /^\w{6,}$/;
//
//        if ($(this).has(".form-control")) {
//            if ($(this).val().length > 0) {
//                switch ($(this).attr("type")) {
//                    case "text":
//                        if (!regName.test($(this).val())) {
//                            $(this).closest('.form-group').addClass('has-warning');
//                        }
//                        else {
//                            $(this).closest('.form-group').removeClass('has-warning');
//                            $(this).closest('.form-group').removeClass('has-error');
//                        }
//                        break;
//                    case "email":
//                        if (!regEmail.test($(this).val())) {
//                            $(this).closest('.form-group').addClass('has-warning');
//                        }
//                        else {
//                            $(this).closest('.form-group').removeClass('has-warning');
//                            $(this).closest('.form-group').removeClass('has-error');
//                        }
//                        break;
//                    case "password":
//                        if (!regPassword.test($(this).val())) {
//                            $(this).closest('.form-group').addClass('has-warning');
//                        }
//                        else {
//                            $(this).closest('.form-group').removeClass('has-warning');
//                            $(this).closest('.form-group').removeClass('has-error');
//                        }
//                        break;
//                    default:
//                        break;
//                }
//            }
//            else {
//                $(this).closest('.form-group').addClass('has-error');
//            }
//        }
//        else {
//            $(this).closest('.form-group').addClass('has-error');
//        }
//    });
//
//    $("table td").click(function () {
//        var row = $(this).parent().index() + 1;
//        //var col = $(this).index() + 1;
//
//        var parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9;
//
//        try {
//            parameter1 = $("table").find("tr").eq(row).find("td").eq(0).text();
//            if (parameter1.indexOf("Edit") > 0) parameter1 = "";
//            $(".myparameter1").val( parameter1);
//        } catch (e) {
//        }
//        try {
//            parameter2 = $("table").find("tr").eq(row).find("td").eq(1).text();
//            if (parameter2.indexOf("Edit") > 0) parameter2 = "";
//            $(".myparameter2").val( parameter2);
//        } catch (e) {
//        }
//        try {
//            parameter3 = $("table").find("tr").eq(row).find("td").eq(2).text();
//            if (parameter3.indexOf("Edit") > 0) parameter3 = "";
//            $(".myparameter3").val( parameter3);
//
//
//        } catch (e) {
//        }
//        try {
//            parameter4 = $("table").find("tr").eq(row).find("td").eq(3).text();
//            if (parameter4.indexOf("Edit") > 0) parameter4 = "";
//            $(".myparameter4").val( parameter4);
//        } catch (e) {
//        }
//        try {
//            parameter5 = $("table").find("tr").eq(row).find("td").eq(4).text();
//            if (parameter5.indexOf("Edit") > 0) parameter5 = "";
//            $(".myparameter5").val( parameter5);
//        } catch (e) {
//        }
//        try {
//            parameter6 = $("table").find("tr").eq(row).find("td").eq(5).text();
//            if (parameter6.indexOf("Edit") > 0) parameter6 = "";
//            $(".myparameter6").val( parameter6);
//        } catch (e) {
//        }
//        try {
//            parameter7 = $("table").find("tr").eq(row).find("td").eq(6).text();
//            if (parameter7.indexOf("Edit") > 0) parameter7 = "";
//            $(".myparameter7").val( parameter7);
//        } catch (e) {
//        }
//        try {
//            parameter8 = $("table").find("tr").eq(row).find("td").eq(7).text();
//            if (parameter8.indexOf("Edit") > 0) parameter8 = "";
//            $(".myparameter8").val( parameter8);
//        } catch (e) {
//        }
//        try {
//            parameter9 = $("table").find("tr").eq(row).find("td").eq(8).text();
//            if (parameter9.indexOf("Edit") > 0) parameter9 = "";
//            $(".myparameter9").val( parameter9);
//        } catch (e) {
//        }
//    });
//});
