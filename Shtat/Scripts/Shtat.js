function SotrudnikSelect(IDSot) {

    $.ajax({
        url: "/Home/EditSotrudnik/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })   
}


function SotrudnikEdit(ID) {
    $.ajax({
        url: "/Home/EditSotrudnik/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}


// Редактирование должности //

function DoljEdit(ID) {
    $.ajax({
        url: "/Home/EditDolj/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}

//-------------------------//


// Редактирование разряда МК //

function RazrEdit(ID) {
    $.ajax({
        url: "/Home/EditRazr/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}

//-------------------------//


// Редактирование БТС //

function BtsEdit(ID) {
    $.ajax({
        url: "/Home/EditBts/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}

//-------------------------//



function SotrudnikAdd(ID) {
        $.ajax({
            url: "/Home/AddSotrudnik/",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
                $('#ServicesModal').modal('show');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }

        })
    }

//Добавление должности//

    function DoljAdd(ID) {
        $.ajax({
            url: "/Home/AddDolj/",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
                $('#ServicesModal').modal('show');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }

        })
    }

//----------------------//

//Добавление разряда МК//

    function RazrAdd(ID) {
        $.ajax({
            url: "/Home/AddRazr/",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
                $('#ServicesModal').modal('show');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }

        })
    }

//---------------------//


//Добавление БТС//

    function BtsAdd(ID) {
        $.ajax({
            url: "/Home/AddBts/",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
                $('#ServicesModal').modal('show');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }

        })
    }

//----------------------//

    function SotrudnikAddSave(Sotrudnik1) {

        var isValid = true;
    if ($('#sotrudnik1').val() == "") {
        $('#sotrudnik1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#sotrudnik1').css('border-color', 'lightgrey');
    }
    

    if (isValid == false) {
        return false;
    }

    var data = {
        //'ID': ID,
        'Sotrudnik1': $('#sotrudnik1').val()
       
    };

    $.ajax({
        url: "/Home/SotrudnikAddSave",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


//Сохранение добавления должности//

    function DoljAddSave(doljnost) {

        var isValid = true;
        if ($('#doljnost').val() == "") {
            $('#doljnost').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#doljnost').css('border-color', 'lightgrey');
        }

        if ($('#datvvoda').val() == "") {
            $('#datvvoda').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#datvvoda').css('border-color', 'lightgrey');
        }

        if (isValid == false) {
            return false;
        }

        var data = {
            //'ID': ID,
            'doljnost': $('#doljnost').val(),
            'dat': $('#datvvoda').val()
        };

        $.ajax({
            url: "/Home/DoljAddSave",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(data),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

    }

//--------------------------------//


//Сохранение добавления разряда МК//

    function RazrAddSave(razr) {

        var isValid = true;

        if ($('#razr').val() == "") {
            $('#razr').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#razr').css('border-color', 'lightgrey');
        }

        if ($('#ETC').val() == "") {
            $('#ETC').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#ETC').css('border-color', 'lightgrey');
        }

        if ($('#mk').val() == "") {
            $('#mk').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#mk').css('border-color', 'lightgrey');
        }

        if ($('#datvvoda').val() == "") {
            $('#datvvoda').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#datvvoda').css('border-color', 'lightgrey');
        }

        if (isValid == false) {
            return false;
        }

        var data = {
            //'ID': ID,
            'razr': $('#razr').val(),
            'ETC': $('#ETC').val(),
            'mk': $('#mk').val(),
            'dat': $('#datvvoda').val()
        };

        $.ajax({
            url: "/Home/RazrAddSave",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(data),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

    }

//--------------------------------//


//Сохранение добавления БТС//

    function BtsAddSave(gruppa, bts, datvvoda) {

        var isValid = true;

        if ($('#gruppa').val() == "") {
            $('#gruppa').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#gruppa').css('border-color', 'lightgrey');
        }

        if ($('#bts').val() == "") {
            $('#bts').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#bts').css('border-color', 'lightgrey');
        }
                
        if ($('#datvvoda').val() == "") {
            $('#datvvoda').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#datvvoda').css('border-color', 'lightgrey');
        }

        if (isValid == false) {
            return false;
        }

        var data = {
            'gruppa': $('#gruppa').val(),
            'bts': $('#bts').val(),
            'dat': $('#datvvoda').val()
        };

        $.ajax({
            url: "/Home/BtsAddSave",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(data),
            dataType: "html",
            success: function (result) {
                $('#ServicesModalContent').html(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

    }


//--------------------------------//

function DeleteSelectSotrudnik(ID) {
    $.ajax({
        url: "/Home/SelectDeleteSotrudnik/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
 

// удаление с запросом должности//

function DeleteSelectDolj(ID) {
    $.ajax({
        url: "/Home/SelectDeleteDolj/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//-----------------------------//

// удаление с запросом разряда МК//

function DeleteSelectRazr(ID) {
    $.ajax({
        url: "/Home/SelectDeleteRazr/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//-----------------------------//

// удаление с запросом БТС//

function DeleteSelectBts(ID) {
    $.ajax({
        url: "/Home/SelectDeleteBts/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//-----------------------------//


function DeleteSelectVac(ID) {
    $.ajax({
        url: "/Home/SelectDeleteVac/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function SotrudnikSave(ID, Sotrudnik1) {

    //var isValid = true;
    //if ($('#sotrudnik1').val() == "") {
    //    $('#sotrudnik1').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#sotrudnik1').css('border-color', 'lightgrey');
    //}
    

    //if (isValid == false) {
    //    return false;
    //}

    var data = {
        'ID': ID,
        'Sotrudnik1': $('#sotrudnik1').val()
        
    };

    $.ajax({
        url: "/Home/SotrudnikSave",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

// сохранение редактирования должности//

function DoljSave(ID, Doljnost1, Datein, Dateout) {

    
    var data = {
        'ID': ID,
        'Doljnost1': $('#doljnost').val(),
        'Datein': $('#datvvoda').val(),
        'Dateout': $('#datzak').val()
    };

    $.ajax({
        url: "/Home/DoljSave",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
//-----------------------------------//

// сохранение редактирования разряда МК//

function RazrSave(ID, Razr1, RazrETC, MK, Datein, Dateout) {


    var data = {
        'ID': ID,
        'Razr1': $('#razr').val(),
        'RazrETC': $('#ETC').val(),
        'MK': $('#mk').val(),
        'Datein': $('#datvvoda').val(),
        'Dateout': $('#datzak').val()
    };

    $.ajax({
        url: "/Home/RazrSave",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
//-----------------------------------//

// сохранение редактирования БТС//

function BtsSave(ID, Gruppa, Stavka, Datein, Dateout) {


    var data = {
        'ID': ID,
        'Gruppa': $('#gruppa').val(),
        'Stavka': $('#bts').val(),
        'Datein': $('#datvvoda').val(),
        'Dateout': $('#datzak').val()
    };

    $.ajax({
        url: "/Home/BtsSave",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
//-----------------------------------//


function DeleteSelectSotrudnik(ID) {
    $.ajax({
        url: "/Home/SelectDeleteSotrudnik/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteSotrudnik(ID) {


    $.ajax({
        url: "/Home/DeleteSotrudnik/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//Удаление должности //

function DeleteDolj(ID) {

    $.ajax({
        url: "/Home/DeleteDolj/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//-------------------//


//Удаление разряда МК //

function DeleteRazr(ID) {


    $.ajax({
        url: "/Home/DeleteRazr/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//-------------------//

//Удаление БТС //

function DeleteBts(ID) {


    $.ajax({
        url: "/Home/DeleteBts/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//-------------------//


function DeleteVac(ID) {


    $.ajax({
        url: "/Home/DeleteVac/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

            //Работа с таблицей Vacancy//

function IndexEdite(ID) {
    $.ajax({
        url: "/Home/IndexEdit/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}

function AddZapis(ID) {
    $.ajax({
        url: "/Home/IndexAdd/"+ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
            $('#ServicesModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}


function SaveIndex() {

    var isValid = true;

    if ($('#aup').val() == "") {
        $('#aup').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#aup').css('border-color', 'lightgrey');
    }

    if ($('#kol').val() == "") {
        $('#kol').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#kol').css('border-color', 'lightgrey');
    }

    
    if ($('#c_093_107').val() == "") {
        $('#c_093_107').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#c_093_107').css('border-color', 'lightgrey');
    }
      

    if ($('#zaviskv').val() == "") {
        $('#zaviskv').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zaviskv').css('border-color', 'lightgrey');
    }

    if ($('#zatehvid').val() == "") {
        $('#zatehvid').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zatehvid').css('border-color', 'lightgrey');
    }

    if ($('#zaharspec').val() == "") {
        $('#zaharspec').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zaharspec').css('border-color', 'lightgrey');
    }

    if ($('#zafilial').val() == "") {
        $('#zafilial').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zafilial').css('border-color', 'lightgrey');
    }

    if ($('#zakateg').val() == "") {
        $('#zakateg').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zakateg').css('border-color', 'lightgrey');
    }

    if ($('#zastar').val() == "") {
        $('#zastar').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zastar').css('border-color', 'lightgrey');
    }

    //if ($('#okladpov').val() == "") {
    //    $('#okladpov').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#okladpov').css('border-color', 'lightgrey');
    //}

    if ($('#zakontrakt').val() == "") {
        $('#zakontrakt').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zakontrakt').css('border-color', 'lightgrey');
    }

    if ($('#za1748').val() == "") {
        $('#za1748').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#za1748').css('border-color', 'lightgrey');
    }

    //if ($('#okladdol').val() == "") {
    //    $('#okladdol').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#okladdol').css('border-color', 'lightgrey');
    //}

    if ($('#zaprof').val() == "") {
        $('#zaprof').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zaprof').css('border-color', 'lightgrey');
    }

    if ($('#zavisdost').val() == "") {
        $('#zavisdost').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zavisdost').css('border-color', 'lightgrey');
    }

    if ($('#proch').val() == "") {
        $('#proch').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#proch').css('border-color', 'lightgrey');
    }

    //if ($('#itog').val() == "") {
    //    $('#itog').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#itog').css('border-color', 'lightgrey');
    //}

    if ($('#datain').val() == "") {
        $('#datain').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#datain').css('border-color', 'lightgrey');
    }

    if (isValid == false) {
        return false;
    }

    var data = {

        'IDPodrPr': $('#pppp').val(),
        'IDPodrOtd': $('#otdel').val(),
        'IDPodrPodr': $('#gruppa').val(),
        'IDPodrUch': $('#uch').val(),
        'IDDolj': $('#dolj').val(),
        'IDSotr': $('#sot').val(),
        'IDStav': $('#stav').val(),
        'AUP': $('#aup').val(),
        'Kol': $('#kol').val(),
        'C_093_107': $('#c_093_107').val(),
        'IDRazr': $('#razr').val(),
        'KtVisKval': $('#zaviskv').val(),
        'KtTehVid': $('#zatehvid').val(),
        'KtHarSpec': $('#zaharspec').val(),
        'KtFilial': $('#zafilial').val(),
        'KtZaKateg': $('#zakateg').val(),
        'KtZaStarsh': $('#zastar').val(),
        'KtZaContract': $('#zakontrakt').val(),
        'Post1748': $('#za1748').val(),
        'KtZaProfMast': $('#zaprof').val(),
        'KtZaVisDostij': $('#zavisdost').val(),
        'KtProchaya': $('#proch').val(),
        'OKRB': $('#kod').val(),
        //'Itog': $('#itog').val()
        //'Datein': $('#datain').val(),
        //'Dateout': $('#dataout').val(),
        //'Priznak': $('#priznak').val()

    };

    $.ajax({
        url: "/Home/IndexSave",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

// Редактирование вакансии

function SaveIndexEdite() {

    var isValid = true;

    if ($('#AUP').val() == "") {
        $('#AUP').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AUP').css('border-color', 'lightgrey');
    }

    if ($('#Kol').val() == "") {
        $('#Kol').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Kol').css('border-color', 'lightgrey');
    }

    
    if ($('#C_093_107').val() == "") {
        $('#C_093_107').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#C_093_107').css('border-color', 'lightgrey');
    }
      

    if ($('#zaviskv').val() == "") {
        $('#zaviskv').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zaviskv').css('border-color', 'lightgrey');
    }

    if ($('#zatehvid').val() == "") {
        $('#zatehvid').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zatehvid').css('border-color', 'lightgrey');
    }

    if ($('#zaharspec').val() == "") {
        $('#zaharspec').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zaharspec').css('border-color', 'lightgrey');
    }

    if ($('#zafilial').val() == "") {
        $('#zafilial').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zafilial').css('border-color', 'lightgrey');
    }

    if ($('#zakateg').val() == "") {
        $('#zakateg').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zakateg').css('border-color', 'lightgrey');
    }

    if ($('#zastar').val() == "") {
        $('#zastar').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zastar').css('border-color', 'lightgrey');
    }

    if ($('#okladpov').val() == "") {
        $('#okladpov').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#okladpov').css('border-color', 'lightgrey');
    }

    if ($('#zakontrakt').val() == "") {
        $('#zakontrakt').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zakontrakt').css('border-color', 'lightgrey');
    }

    if ($('#za1748').val() == "") {
        $('#za1748').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#za1748').css('border-color', 'lightgrey');
    }

    if ($('#okladdol').val() == "") {
        $('#okladdol').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#okladdol').css('border-color', 'lightgrey');
    }

    if ($('#zaprof').val() == "") {
        $('#zaprof').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zaprof').css('border-color', 'lightgrey');
    }

    if ($('#zavisdost').val() == "") {
        $('#zavisdost').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#zavisdost').css('border-color', 'lightgrey');
    }

    if ($('#proch').val() == "") {
        $('#proch').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#proch').css('border-color', 'lightgrey');
    }

    if ($('#itog').val() == "") {
        $('#itog').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#itog').css('border-color', 'lightgrey');
    }

    if ($('#datain').val() == "") {
        $('#datain').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#datain').css('border-color', 'lightgrey');
    }

    if (isValid == false) {
        return false;
    }

    var data = {

        'ID': $('#ID').val(),
        'IDPodrPr': $('#Podrazd').val(),
        'IDPodrOtd': $('#otdel').val(),
        'IDPodrPodr': $('#gruppa').val(),
        'IDPodrUch': $('#uch').val(),
        'KOD' : $('#kod').val(),
        'IDDolj': $('#dolj').val(),
        'IDSotr': $('#sot').val(),
        'IDStav': $('#stav').val(),
        'AUP': $('#AUP').val(),
        'Kol': $('#Kol').val(),
        'VsegoZarpl': $('#vsego').val(),
        'C_093_107': $('#C_093_107').val(),
        'IDRazr': $('#razr').val(),
        'KtVisKval': $('#zaviskv').val(),
        'KtTehVid': $('#zatehvid').val(),
        'KtHarSpec': $('#zaharspec').val(),
        'KtFilial': $('#zafilial').val(),
        'KtZaKateg': $('#zakateg').val(),
        'KtZaStarsh': $('#zastar').val(),
        'OkladPovish': $('#okladpov').val(),
        'KtZaContract': $('#zakontrakt').val(),
        'Post1748': $('#za1748').val(),
        'DoljnOklad': $('#okladdol').val(),
        'KtZaProfMast': $('#zaprof').val(),
        'KtZaVisDostij': $('#zavisdost').val(),
        'KtProchaya': $('#proch').val(),
        'Itog': $('#itog').val()
        //'Datein': $('#datain').val(),
        //'Dateout': $('#dataout').val(),
        //'Priznak': $('#priznak').val()

    };

    $.ajax({
        url: "/Home/IndexSaveEdite",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalContent').html(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
// Запись вакансий в историю //

function SaveHistory() {

    var isValid = true;

    if ($('#datapicker').val() == "") {
        $('#datapicker').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#datapicker').css('border-color', 'lightgrey');
    }
      
    if (isValid == false) {
        return false;
    }

    var data = {

        'dat': $('#datapicker').val(),
    }

    $.ajax({
        url: "/Home/SaveHistory",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


//   Выбор записей в истории   //

function SaveHisDat() {

    var isValid = true;

    if ($('#HD').val() == "") {
        $('#HD').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#HD').css('border-color', 'lightgrey');
    }

    if (isValid == false) {
        return false;
    }

    var data = {

        'dat': $('#HD').val(),
    }

    $.ajax({
        url: "/Home/SaveHisDat",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#tab').html(result);
            //$('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

//Фильтр по подразделениям //

function SaveVac() {

    var isValid = true;

    if ($('#pppp').val() == "") {
        $('#pppp').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#pppp').css('border-color', 'lightgrey');
    }

    if (isValid == false) {
        return false;
    }

    var data = {

        'IDPodrPr': $('#pppp').val(),
        'IDPodrOtd': $('#otdel').val(),
        'IDPodrPodr': $('#gruppa').val(),
        'IDPodrUch': $('#uch').val()
    }

    $.ajax({
        url: "/Home/SaveVac",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#tab').html(result);
            //$('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

//Фильтр по должностям!!! //

function SaveDolj() {

    var isValid = true;

    if ($('#doljn').val() == "") {
        $('#doljn').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#doljn').css('border-color', 'lightgrey');
    }

    if (isValid == false) {
        return false;
    }

    var data = {

        'IDdoljn': $('#doljn').val()
        
    }

    $.ajax({
        url: "/Home/SaveDolj",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#tab').html(result);
            //$('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

//----------------------------------------------------//

function SaveH() {

    var isValid = true;

    //if ($('#pppp').val() == "") {
    //    $('#pppp').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#pppp').css('border-color', 'lightgrey');
    //}

    if (isValid == false) {
        return false;
    }

    var data = {
        'dat': $('#HD').val(),
        'IDPodrPr': $('#pppp').val(),
        'IDPodrOtd': $('#otdel').val(),
        'IDPodrPodr': $('#gruppa').val(),
        'IDPodrUch': $('#uch').val(),
        
    }

    $.ajax({
        url: "/Home/SaveH",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#tab').html(result);
            //$('#ServicesModalDelete').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


//--------------------------------------------------------------------------//
//Тестирование вывода списка в группировке Group By

function SortSort() {

    var isValid = true;

    
    if (isValid == false) {
        return false;
    }

    var data = {
        'dat': $('#HD').val(),
        'IDPodrPr': $('#pppp').val(),
        'IDPodrOtd': $('#otdel').val(),
        'IDPodrPodr': $('#gruppa').val(),
        'IDPodrUch': $('#uch').val(),

    }

    $.ajax({
        url: "/Home/SortSort",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result) {
            $('#ServicesModalDeleteContent').html(result);
            $('#ServicesModalDelete').modal('show');
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
//--------------------------------------------------------------------------//

function loadReport()
{
    // window.location = "/Home/Export/";
    var stringhref = "Export?";

    stringhref += "dat=" + $('#HD').val() + "&" + "IDPodrPr=" + $('#pppp').val() + "&" + "IDPodrOtd=" + $('#otdel').val() + "&" + "IDPodrPodr=" + $('#gruppa').val() + "&" + "IDPodrUch=" + $('#uch').val();
    //window.open(stringhref, '_blank');
    window.location = stringhref;
    

}


//Формирование отчета (передача параметров)//
function loadReport2() {

    var data = {
        'dat': $('#HD').val(),
        'IDPodrPr': $('#pppp').val(),
        'IDPodrOtd': $('#otdel').val(),
        'IDPodrPodr': $('#gruppa').val(),
        'IDPodrUch': $('#uch').val(),

    }

    $.ajax({
        url: "/Home/Export",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(data),
        dataType: "html",
        success: function (result)
        {

        }
      //      $('#tab').html(result);
      //      $('#ServicesModalDelete').modal('show');
      //  },
      //error: function (errormessage) {
      //      alert(errormessage.responseText);
      //  }
    });

}


//----------Отчет сгруппированный ------------------------------------------------
function GroupReport() {
    // window.location = "/Home/Export/";
    var stringhref = "ExportGroup?";

    stringhref += "dat=" + $('#HD').val() + "&" + "IDPodrPr=" + $('#pppp').val() + "&" + "IDPodrOtd=" + $('#otdel').val() + "&" + "IDPodrPodr=" + $('#gruppa').val() + "&" + "IDPodrUch=" + $('#uch').val();
    //window.open(stringhref, '_blank');
    window.location = stringhref;
    //window.location = "/Home/ExportGroup/";
    


}
//----------------------------------------------------------