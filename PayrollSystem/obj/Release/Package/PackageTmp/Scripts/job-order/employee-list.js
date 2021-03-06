﻿
$(document).ready(function () {
           
    $(".pagination li").click(function(e){
        e.preventDefault();

        var page = $(this).text();
        var search_value = $("#search_value").val();

        if (page === "»") {
            if(@Model.PageNumber + 10 >= @Model.PageCount)
                page = @Model.PageCount;
            else
                page = @Model.PageNumber + 10;
        }
        if (page === "«"){
            if(@Model.PageNumber - 10 >= 1)
                page = @Model.PageNumber - 10;

            else
                page = 1;
        }

        if (page === "»»") page = @Model.PageCount;
        if (page === "««") page = 1;

               
        alert(getPageId()+" "+page+" "+search_value);
        loadJoEmployee(getPageId(), page, search_value);

    });

    $('.create_jo_payroll').click(function () {

        var id = $(this).closest('tr').find('td:eq(0)').text().trim();
        var firstname = $(this).closest('tr').find('td:eq(1)').text().trim();
        var middlename = $(this).closest('tr').find('td:eq(2)').text().trim();
        var lastname = $(this).closest('tr').find('td:eq(3)').text().trim();
        var fullname = firstname + " " + middlename + " " + lastname;

        var salary = $(this).closest('tr').find('td:eq(5)').text().trim();

        var coop = $(this).closest('tr').find('td:eq(6)').text().trim();
        var pagibig = $(this).closest('tr').find('td:eq(7)').text().trim();
        var gsis = $(this).closest('tr').find('td:eq(8)').text().trim();
        var phic = $(this).closest('tr').find('td:eq(9)').text().trim();
        var excess = $(this).closest('tr').find('td:eq(10)').text().trim();

        $(".selected").removeClass("selected");
        $("." + id).addClass("selected");

        $('#box_create_payroll').removeClass("hide");
        $('#box_jo_payroll_list').removeClass("hide");
        $(".parent-payroll-list").html("Loading......");
        loadJoPayrollList(1, id, firstname, lastname, middlename);

        $("#jo_salary").val(formatComma(salary));
        $("#jo_id").val(id);
        $("#jo_name").val(fullname);

        $("#jo_coop").val(formatComma(coop.split('/')[0]));
        $("#jo_coop_payment").val(coop.split('/')[1]);
        $("#jo_coop_paid").val(coop.split('/')[2]);

        $("#jo_pagibig").val(formatComma(pagibig.split('/')[0]));
        $("#jo_pagibig_payment").val(pagibig.split('/')[1]);
        $("#jo_pagibig_paid").val(pagibig.split('/')[2]);

        $("#jo_gsis").val(formatComma(gsis.split('/')[0]));
        $("#jo_gsis_payment").val(gsis.split('/')[1]);
        $("#jo_gsis_paid").val(gsis.split('/')[2]);

        $("#jo_phic").val(formatComma(phic.split('/')[0]));
        $("#jo_phic_payment").val(phic.split('/')[1]);
        $("#jo_phic_paid").val(phic.split('/')[2]);

        $("#jo_excess").val(formatComma(excess.split('/')[0]));
        $("#jo_excess_payment").val(excess.split('/')[1]);
        $("#jo_excess_paid").val(excess.split('/')[2]);

        var choosen_month = $("#jo_month").val();
        var choosen_year = $("#jo_year").val();
        if (choosen_month <= 9) choosen_month = "0" + choosen_month;
        var choosen_options = $("#jo_month_range").val();

        if (choosen_month != "00" && choosen_year != "0" && choosen_options != "0") {

            switch (choosen_options) {
                case "1":
                    var from = choosen_year + "-" + choosen_month + "-01";
                    var to = choosen_year + "-" + choosen_month + "-15";

                    $("#jo_date_range").val(choosen_month + "/01/" + choosen_year + "-" + choosen_month + "/15/" + choosen_year);
                    getLateAndAbsences(id, from, to);
                    break;
                case "2":
                    var from = choosen_year + "-" + choosen_month + "-16";
                    var to = choosen_year + "-" + choosen_month + "-" + daysInMonth(parseInt(choosen_month), choosen_year);

                    $("#jo_date_range").val(choosen_month + "/16/" + choosen_year + "-" + choosen_month + "/" + daysInMonth(parseInt(choosen_month), choosen_year) + "/" + choosen_year);
                    getLateAndAbsences(id, from, to);
                    break;
            }
        }

    });

});

