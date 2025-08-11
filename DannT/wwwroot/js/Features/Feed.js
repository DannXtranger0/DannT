import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let form = document.querySelector("form");  
export let selectTagId = document.getElementById("TagId");

let table = document.querySelector("tbody");
let cbCompleted = document.getElementById("CbCompleted");
let cbPending = document.getElementById("CbPending");

export async function Search(form) {
    let formData = formToObject(form);
    formData.CbCompleted = cbCompleted.checked;
    formData.CbPending = cbPending.checked;
    formData.TagId = selectTagId.options[selectTagId.selectedIndex].value;

    console.log(formData);
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
    table.querySelectorAll("tr:not(.table-headers)").forEach(x => x.remove());
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
    input.classList.add("cb-search");
    if (currentStatus == "Completed")
        input.checked = true;

    addFuncionalityCheckBox(input, taskId)

    td.appendChild(input);
    row.appendChild(td);
}
async function addFuncionalityCheckBox(input,taskId) {
    input.addEventListener("change", async () => {
        let sound;
        if (input.checked == true) 
            sound =new Audio("/audio/check.mp3");
         else
            sound = new Audio("/audio/descheck.mp3");

        sound.play();

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

    anchorEdit.href = `/Task/${taskId}`;

    anchorEdit.classList.add("btn-action");
    buttonDelete.classList.add("btn-action");

    anchorEdit.classList.add("bg-pencil");
    buttonDelete.classList.add("bg-delete");
    addFuncionalityDeleteButton(buttonDelete, taskId);

    let div = document.createElement("div");
    div.classList.add("actions-container");

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