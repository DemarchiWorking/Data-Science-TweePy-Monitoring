import { Link } from 'react-router-dom';
import Header from '../../../components/Header';
import './styles.css';
import React, { useState, useEffect } from 'react';
import api from '../../../services/api';



export function TopFollowersUsersList() {
    const [users, setUsers] = useState([]);
   
   
    useEffect(() => {
        
        api.get(`http://localhost:8088/funcoes/usuarios-top-seguidores`)
            .then((response) => {
                setUsers(response.data.list);
            })
            .catch((err) => {
                alert("Erro ao fazer a requisicao :" + err);
            })
    }, []);

for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
            <div className='main'>
                   
                <table style={{margin:'auto'}}>
                    <thead>
                        <tr>
                            <th> Rank </th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Id Usuário </th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Usuário </th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Seguidores</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Seguindo</th>   
                        </tr>
                    </thead>
                    <tbody>
                        {users?.map((user, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#538AF0' : '#fff' }}>
                                    <td> {index+1} </td>                                    
                                    <td style={{ width: '170px', textAlign: 'center' }}> {user.idUser}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {user.username}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{user.followersCount}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{user.followedCount}</td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 


        </>
    );       
} 
}

export function PostsGroupedByHour() {
    const [tweetsPerHour, setTweetsPerHour] = useState([]);
   

    useEffect(() => {
        
        api.get(`http://localhost:8088/funcoes/tweets-por-hora`)
            .then((response) => {
                setTweetsPerHour(response.data.list);
                console.log(response.data);
            })
            .catch((err) => {
                alert("Erro :" + err);
            })
    }, []);

for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
            <div className='main'>                                  
                <table style={{margin:'auto'}}>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}> Hora </th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Quantidade Tweets</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tweetsPerHour?.map((tweetPerHour, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#538AF0' : '#fff' }}>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {tweetPerHour.hour}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {tweetPerHour.count}</td>
                                   
                                </tr>


                            )
                        })}

                    </tbody>
                </table>                
            </div>

        </>
    );       
} 
}

export function NumberTweetPerLanguage() {
    const [tweetsPerTagAndLang, setTweetPerTagAndLang] = useState([]);
   

    useEffect(() => {
        
        api.get(`http://localhost:8088/funcoes/quantidade-tweets-por-idioma`)
            .then((response) => {
                setTweetPerTagAndLang(response.data.list);
                console.log(response.data);
            })
            .catch((err) => {
                alert("Erro :" + err);
                console.log('errado');
            })
    }, []);

for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>                    
                <table style={{margin:'auto'}}>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}> Tag </th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Idioma </th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Quantidade Tweets </th>
                        </tr>
                    </thead>
                    <tbody>
                        {tweetsPerTagAndLang?.map((tweetPerTagAndLang, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#538AF0' : '#fff' }}>
                                                     
                                    <td style={{ width: '170px', textAlign: 'center' }}> {tweetPerTagAndLang.tag}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {tweetPerTagAndLang.lang}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{tweetPerTagAndLang.count}</td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>


        </>
    );       
} 
}
