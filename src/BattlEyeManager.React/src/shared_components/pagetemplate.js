import React from 'react';
import { MainMenu } from "./mainmenu";

export const PageTemplate = ({children}) =>
<div>
    <MainMenu />
    <div className="container">
        {children}
    </div>
</div>