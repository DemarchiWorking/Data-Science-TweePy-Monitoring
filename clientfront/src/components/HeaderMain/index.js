import React from 'react'

import { Link } from 'react-router-dom'

import './headerMain.css'

function HeaderMain() {
    return (

        <header>
            <div className="container">
                <div className="logo" >
                    <Link to="/"><h1>CTweetpy</h1></Link>
                </div>

                <div className="btn-newPost" >

                    <a href="https://github.com/DemarchiWorking/DataScienceTweepy" target="_blank"><button> GitHub </button></a>
                   

                </div>
            </div>
        </header>

    )
}

export default HeaderMain