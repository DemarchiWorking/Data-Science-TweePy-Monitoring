import { BrowserRouter, Routes, Route } from 'react-router-dom';

import { Home } from './pages/home'
import { TopFollowersUsersList, PostsGroupedByHour, NumberTweetPerLanguage } from './pages/functions/result';

function RoutesApp(){

return (
    <BrowserRouter>
        <Routes>
            <Route path='/' element={ <Home/> }/>

            <Route path="/funcoes/usuarios-top-seguidores" element={ <TopFollowersUsersList/>}/>
            <Route path="/funcoes/tweets-por-hora" element={ <PostsGroupedByHour/>}/>
            <Route path="/funcoes/quantidade-tweets-por-idioma" element={ <NumberTweetPerLanguage/>}/>

        </Routes>
    </BrowserRouter>
)
    
}
export default RoutesApp;