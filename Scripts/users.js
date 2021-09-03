var companies;
//var userRoles;
//var users;
//var user_comp;

$(document).ready(function () {
    var url = "/Company/GetCompanies/";
    $.get(url, null, function (data) {
        var array = data.data
        companies = array;
    })
});

//$(document).ready(function () {
//    var url = "/User/GetUsers/";
//    $.get(url, null, function (data) {
//        var array = data.data
//        users = array;
//    })
//});

//$(document).ready(function () {
//    var url = "/Company/GetUserRoles/";
//    $.get(url, null, function (data) {
//        var array = data.data
//        userRoles = array;
//    })
//});

//function user_map() {
//    users.forEach(function (usr) {
//        companies.forEach(function (cmp) {
//            if (user.CompanyId == cmp.Id) {
//                user.companies = cmp;
//                user_comp.push(usr);
//            }
//        });
//    });
//}

//$(document).ready(function () {
//    user_map();
//    console(user_comp);
//});



$(document).ready(function () {
    var url = "/User/GetUsers/";
    $.get(url, null, function (data) {
        var array = data.data
        if (array) {
            array.forEach(function (elem) {
                var html = `
                    <tr class="user_member" data-id="${elem.Id}">
                        <td>${elem.Email}</td>
                        <td>${elem.RoleId}</td>
                        <td>${elem.NameAndSurname}</td>
                        <td>${elem.CompanyId}</td>
                        <td>${elem.CompanyId}</td>
                        <td>${elem.CompanyId}</td>
                        <td>${elem.CompanyId}</td>
                        <td>
                            <button type="button" class="see btn btn-primary" data-toggle="modal" data-id="${elem.Id}" data-target="#seeModal"><i class="fas fa-eye"></i></button>
                            <button type="button" class="update btn btn-warning" data-toggle="modal" data-id="${elem.Id}" data-target="#updateModal"><i class="fas fa-pen"></i></button>
                            <button type="button" class="delete btn btn-danger" data-id="${elem.Id}"><i class="far fa-trash-alt"></i></button>
                        </td>
                    </tr>
`
                $('#tableBody').append(html)
            }, this);
        }
    })
});

$(document).on('click', '.update', function () {
    var url = "/User/PopulateModal/";
    var id = $(this).attr("data-id");
    $.get(url, { id: id }, function (data) {
        $('#inputStateCompanyUpdate').html();
        $("#inputEmailUpdate").val(data.data.Email);
        $("#inputNameUpdate").val(data.data.NameAndSurname);
        $('#inputStateRoleUpdate').val(data.data.RoleId);
        $("#updateId").val(data.data.Id);
        $('#inputStateCompanyUpdate').val(0);
        companies.forEach(function (elem) {
            var selected = ""
            if (elem.Id == data.data.CompanyId) {
                selected = "selected";
            }
            $('#inputStateCompanyUpdate').append(`<option ${selected} value="${elem.Id}" data-id=${elem.Id}>${elem.Name}</option>`)
        }, this);

    })
});

$(document).on('click', '.see', function () {
    var url = "/User/PopulateModal/";
    var id = $(this).attr("data-id");
    $.get(url, { id: id }, function (data) {
        var myhtml = `

                        <div>
                            <div>
                                <span>
                                    <b>Id:</b>
                                </span>

                                <span>
                                    ${data.data.Id}
                                </span>
                            </div >

                            <div>
                                <span><b>E-mail: </b></span>
                                <span>${data.data.Email}</span>
                            </div>

                            <div>
                                <span><b>Role: </b></span>
                                <span>${data.data.RoleId}</span>
                            </div>

                            <div>
                                <span><b>NameSurname: </b></span>
                                <span>${data.data.NameAndSurname}</span>
                            </div>

                            <div>
                                <span><b>CompanyName: </b></span>
                                <span>${data.data.CompanyId}</span>
                            </div>

                            <div>
                                <span><b>CompanyAddress: </b></span>
                                <span>${data.data.CompanyId}</span>
                            </div>

                            <div>
                                <span><b>TaxNo: </b></span>
                                <span>${data.data.CompanyId}</span>
                            </div>

                            <div>
                                <span><b>Phone: </b></span>
                                <span>${data.data.CompanyId}</span>
                            </div>

                        </div >`

        $("#seeModalInner").html(myhtml);
    });
});

$(document).on('click', '.delete', function () {
    var url = "/User/Delete/";
    var id = $(this).attr("data-id");
    var remove_item = $(this).closest(".user_member");
    $.get(url, { id: id }, function (data) {
        if (data.data) {
            remove_item.fadeOut(300, function () {
                remove_item.remove();
            })
        }
    })
});

$(document).ready(function () {
    var url = "/Company/GetCompanies/";
    $.get(url, null, function (data) {
        var array = data.data
        if (array) {
            array.forEach(function (elem) {
                $('#inputStateCompany').append(`<option data-id=${elem.Id}>${elem.Name}</option>`)
            }, this);
        }
    })
});

$(document).ready(function () {
    var url = "/Company/GetUserRoles/";
    $.get(url, null, function (data) {
        var array = data.data
        if (array) {
            array.forEach(function (elem) {
                $('#inputStateRole').append(`<option data-id=${elem.Id}>${elem.Name}</option>`)
            }, this);
        }
    })
});



$(document).ready(function () {
    $("#addNewUser").click(function (event) {
        var formData = {
            Email: $("#inputEmail").val(),
            RoleId: $("#inputStateRole").find(':selected').data('id'),
            NameAndSurname: $("#inputName").val(),
            CompanyId: $("#inputStateCompany").find(':selected').data('id'),
        };

        if (formData) {
            $.ajax({
                type: "POST",
                url: "/User/Create/",
                data: JSON.stringify(formData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 5000,
                success: function (data) {
                    if (data.data) {
                        var user = data.data;
                        var html = `
                        <tr class="user_member" data-id=${user.Id}">
                            <td>${user.Email}</td>
                            <td>${user.RoleId}</td>
                            <td>${user.NameAndSurname}</td>
                            <td>${user.CompanyId}</td>
                            <td>${user.CompanyId}</td>
                            <td>${user.CompanyId}</td>
                            <td>${user.CompanyId}</td>
                            <td>
                                <button type="button" class="see btn btn-primary" data-toggle="modal" data-id="${user.Id}" data-target="#seeModal"><i class="fas fa-eye"></i></button>
                                <button type="button" class="update btn btn-warning" data-toggle="modal" data-id="${user.Id}" data-target="#updateModal"><i class="fas fa-pen"></i></button>
                                <button type="button" class="delete btn btn-danger" data-id="${user.Id}"><i class="far fa-trash-alt"></i></button>
                            </td>
                    </tr>
                    `
                        $('#tableBody').append(html);
                    }
                }
            })
        }

        event.preventDefault();
        $("#inputEmail").val('');
        $("#inputName").val('');
        $('#inputStateRole').val(0);
        $('#inputStateCompany').val(0);

    })
});

$(document).ready(function () {
    var url = "/Company/GetUserRoles/";
    $.get(url, null, function (data) {
        var array = data.data
        if (array) {
            array.forEach(function (elem) {
                $('#inputStateRoleUpdate').append(`<option value="${elem.Id}" data-id=${elem.Id}>${elem.Name}</option>`)
            }, this);
        }
    })
});

$(document).on('click', '#UpdateUser', function () {
    var url = "/User/Update/";
    var id = $(this).attr("data-id");
    var formData = {
        Id: $("#updateId").val(),
        Email: $("#inputEmailUpdate").val(),
        RoleId: $("#inputStateRoleUpdate").find(':selected').data('id'),
        NameAndSurname: $("#inputNameUpdate").val(),
        CompanyId: $("#inputStateCompanyUpdate").find(':selected').data('id'),
    };


    if (formData) {
        $.ajax({
            type: "POST",
            url: "/User/Update/",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 5000,
            success: function (data) {
                if (data.data) {
                    var user = data.data;
                    $("#inputEmailUpdate").val(user.Email),
                    //$("#inputStateRoleUpdate").val(user.RoleId),
                    $("#inputNameUpdate").val(user.NameAndSurname)
                    //$("#inputStateCompanyUpdate").val(user.CompanyId)

                    var html = `
                        <td>${user.Email}</td>
                        <td>${user.RoleId}</td>
                        <td>${user.NameAndSurname}</td>
                        <td>${user.CompanyId}</td>
                        <td>${user.CompanyId}</td>
                        <td>${user.CompanyId}</td>
                        <td>${user.CompanyId}</td>
                        <td>
                            <button type="button" class="see btn btn-primary" data-toggle="modal" data-id="${user.Id}" data-target="#seeModal"><i class="fas fa-eye"></i></button>
                            <button type="button" class="update btn btn-warning" data-toggle="modal" data-id="${user.Id}" data-target="#updateModal"><i class="fas fa-pen"></i></button>
                            <button type="button" class="delete btn btn-danger" data-id="${user.Id}"><i class="far fa-trash-alt"></i></button>
                        </td>
                    `
                    $("#tableBody").find("tr").last().html('');
                    $("#tableBody").find("tr").last().html(html);

                }
            }
        })
    }
});

