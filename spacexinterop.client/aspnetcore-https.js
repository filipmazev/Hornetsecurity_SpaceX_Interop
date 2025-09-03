const fs = require("fs");
const { spawn } = require("child_process");
const path = require("path");

const baseFolder =
    process.env.APPDATA && process.env.APPDATA !== ""
        ? path.join(process.env.APPDATA, "ASP.NET", "https") 
        : path.join(process.env.HOME || "", ".aspnet", "https"); 

console.log(`Using certificate folder: ${baseFolder}`);

if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

const certificateArg = process.argv
    .map((arg) => arg.match(/--name=(?<value>.+)/i))
    .filter(Boolean)[0];

const certificateName = certificateArg
    ? certificateArg.groups.value
    : process.env.npm_package_name;

if (!certificateName) {
    console.error("No certificate name specified.");
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    const child = spawn(
        "dotnet",
        [
            "dev-certs",
            "https",
            "--export-path",
            certFilePath,
            "--format",
            "Pem",
            "--no-password",
        ],
        { stdio: "inherit" }
    );

    child.on("exit", (code) => process.exit(code));
}