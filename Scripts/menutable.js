
    $(document).ready(function () {

        var myMenuLocationTable;

        $(document).on("Click", ".use-ajax", function (e) {
            e.preventDefault();
            var form = $(this).closest('form');
            $(form).submit();
        });

        $.ajax({
            url: "https://localhost:44395/Default/menuListJson",
            type: "GET",
            success: function (result) {
                console.log(result);
                var renderData = "";

                $.each(result, function (id, value) {
                    renderData += '<tr>';
                    renderData += '<td>' + value.Id + '</td>';
                    renderData += '<td>' + value.Location + '</td>';
                    renderData += '<td> <span class="EditMenuLocationList">Edit</span> | <span class="DeleteMenuLocationList">Delete</span> </td>';
                    renderData += '<tr>';
                });

                $('.tbody').html(renderData);

               /* myMenuLocationTable = $('#menuTable').DataTable({
                    "serching": false,
                    "ordering": false,
                    "lengthChange": false
                });*/
            }
        });


        $(document).on('click', '.DeleteMenuLocationList', function () {
            var DID = $(this).parent().parent().childern().eq(0).text();
            var menuObject = {
                Id: DID
            };

            $.ajax({
                url: "https://localhost:44395/Default/delete",
                data: JSON.stringify(menuObject),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    swal("Menu Locatioin Edited!", "You have successfully deleted the Menu Location!", "Success");
                    myMenuLocationTable.destroy();
                    $('tbody').empty();
                    $.ajax({
                        url: "https://localhost:44395/Default/menuListJson",
                        type: "GET",
                        success: function (result) {
                            console.log(result);
                            var renderData = "";
                            $.each(result, function (id, value) {
                                renderData += '<tr>';
                                renderData += '<td>' + value.Id + '</td>';
                                renderData += '<td>' + value.Location + '</td>';
                                renderData += '<td> <span class="EditMenuLocationList">Edit</span> | <span class="DeleteMenuLocationList">Delete</span> </td>';
                                renderData += '<tr>';
                            });

                            $('.tbody').html(renderData);

                            /*myMenuLocationTable = $('#menuTable').DataTable({
                                "serching": false,
                                "ordering": false,
                                "lengthChange": false
                            });*/
                        }
                    });
                }
            });
        });

        $(document).on('click', '.EditMenuLocationList', function () {
            var textNode = $(this).parent().parent().childern()[1].innerHTML;
            var fieldNode = "<input type='text' value='" + textNode + "' />";
            $(this).parent().parent().childern()[1].innerHTML = fieldNode;
            $(this).parent().html('<span class="SaveMenuLocationList">Save</span> | <span class="DeleteMenuLocationList">Delete</span>');
        });

        $(document).on('click', '.SaveMenuLocationList', function () {
            var textNode = $(this).parent().parent().childern().childern().val();
            var id = $(this).parent().parent().childern().eq(0).text();
            $(this).parent().parent().childern()[1].innerHTML = textNode;

            var menuObject = {
                Id: id,
                Location: textNode
            };

            $.ajax({
                url: "https://localhost:44395/Default/save",
                data: JSON.stringify(menuObject),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    swal("Menu Locatioin Edited!", "You have successfully Edited the Menu Location!", "Success");
                    console.log(result);
                },

                error: function (result) {
                    console.log(error);
                }

            });

            $(this).parent().html('<span class="SaveMenuLocationList">Edit</span> | <span class="DeleteMenuLocationList">Delete</span>');

        });       
    });
