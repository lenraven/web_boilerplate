import * as React from "react";
import reactLogo from "../assets/img/react_logo.svg";

interface IProps {
    content: string;
}

export const App = (props: IProps) => {
    return (
        <>
            <div className="flex h-screen justify-center items-center bg-gray-700 text-yellow-500 text-4xl font-bold">
                <div>
                    <img src={reactLogo} className="object-fill h-32 w-full" />
                    <div>{props.content}</div>
                </div>
            </div>
        </>
    );
};