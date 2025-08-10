import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let form = document.querySelector("form");  
let table = document.querySelector("tbody");

export async function Search(form) {
    let formData = formToObject(form);
    let response = await getJson(localDomain + "api/FeedApi/Feed", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    });

    return response;
}
export async function SetTasks(data) {
    data.forEach(elem => {
        let row = document.createElement("tr");

        for (let data in elem) {
            let td = document.createElement("td");

            if (data == "status") {
                 SetStatus(elem[data],td,row,elem["id"]);
                continue;
            }
            if (data == "deadline")
                elem[data] = elem[data].split("T")[0];

            if (data == "id") {
                SetActions(elem[data], td, row);
                continue;
            }

            td.textContent = elem[data];
            row.appendChild(td);
            
        }
        table.appendChild(row);
    })
}

function SetStatus(currentStatus,td,row,taskId) {
    let input = document.createElement("input");
    input.type = "checkbox";

    if (currentStatus == "Completed")
        input.checked = true;

    addFuncionalityCheckBox(input, taskId)

    td.appendChild(input);
    row.appendChild(td);
}
async function addFuncionalityCheckBox(input,taskId) {
    input.addEventListener("change", async () => {
         await getJson(localDomain + `api/TaskApi/UpdateStatus/${taskId}`, {
            method: "PATCH",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Status: input.checked
            })
        });

    })

}
function SetActions(taskId, td, row) {
    let anchorEdit = document.createElement("a");
    let buttonDelete = document.createElement("button");

    anchorEdit.textContent = "Edit";
    anchorEdit.href = `/Task/${taskId}`;
    buttonDelete.textContent = "Delete";
    addFuncionalityDeleteButton(buttonDelete, taskId);

    let div = document.createElement("div");

    div.appendChild(anchorEdit);
    div.appendChild(buttonDelete);

    td.appendChild(div);
    row.appendChild(td);
}


async function addFuncionalityDeleteButton(button, taskId) {
    button.addEventListener("click", async () => {
        await getJson(localDomain + `api/TaskApi/Delete/${taskId}`, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json',
            }
        });

    })

}