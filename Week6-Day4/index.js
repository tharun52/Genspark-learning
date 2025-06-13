const students = [
    { name: "John", age: 21 },
    { name: "Jack", age: 19 },
    { name: "Jake", age: 20 }
];

function fetchStudentsCallback(callback) {
    setTimeout(() => {
        callback(students)
    }, 1000);
}

function getwithCallback() {
    fetchStudentsCallback(students => {
        let output = `<h3>Callback method :</h3><ul>`;
        students.forEach(s => {
            output += `<li>Name: ${s.name}, Age: ${s.age}</li>`;
        });
        output += `</ul>`;
        document.getElementById("output").innerHTML = output;
    });
}




function fetchStudentsPromise() {
    return new Promise(resolve => {
        setTimeout(() => {
            resolve(students);
        }, 1000);
    });
}

function getwithPromise() {
    fetchStudentsPromise().then(students => {
        let output = `<h3>Promise method :</h3><ul>`;
        students.forEach(s => {
            output += `<li>Name: ${s.name}, Age: ${s.age}</li>`;
        });
        output += `</ul>`;
        document.getElementById("output").innerHTML = output;
    });
}




async function fetchStudentsAsyncAwait() {
    return new Promise(resolve => {
        setTimeout(() => {
            resolve(students);
        }, 1000);
    });
}

async function getwithAsyncAwait() {
    const students = await fetchStudentsAsyncAwait();
    let output = `<h3>Async/Await method :</h3><ul>`;
    students.forEach(s => {
        output += `<li>Name: ${s.name}, Age: ${s.age}</li>`;
    });
    output += `</ul>`;
    document.getElementById("output").innerHTML = output;
}