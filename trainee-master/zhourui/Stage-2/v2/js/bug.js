/**
 * Created by Administrator on 2/29/2016.
 */
var obug = {};
obug.Id = 1;
obug.Title = "";
obug.Summary = "";
obug.Description = "";
obug.Type = "";
obug.Project = "";
obug.CreateTime = "2016-02-29";
obug.Creator = "";
obug.Status = "";

var obugs = new Array();

$(document).ready(function () {
    createBug("1", "Bug1", "Bug1's Summary", "Description", "New", "Project1", "2000-01-01", "zhourui", "New");
    createBug("2", "Bug2", "Bug2's Summary", "Description", "Checked", "Project2", "2001-01-01", "zhourui", "Assigned");

    alert(obugs.length);

    for (i = 0; i < obugs.length; i++) {
        $("tbody").append("<tr><td>" + obugs[i].Id + "</td><td>" + obugs[i].Title + "</td></tr>")
        alert(obugs[i].Id + "--" + obugs[i].Title);
    }
});

function createBug(id, title, summary, description, type, project, createtime, creator, status) {
    obug.Id = id;
    obug.Title = title;
    obug.Summary = summary;
    obug.Description = description;
    obug.Type = type;
    obug.Project = project;
    obug.CreateTime = createtime;
    obug.Creator = creator;
    obug.Status = status;

    obugs.push(obug);
    //obugs.add(obug);
}