import { Link } from 'react-router-dom';
import HeaderMain from '../../components/HeaderMain';
import './styles.css';
import api from '../../services/api';
import More from '../../images/more.svg';

export function Home() {
    return (
        <div>
            <HeaderMain/>
            
            
        <p></p>
            <main>
                <div className="cards">
                    <div className="card">

                        <header>
                            <h2> Usuarios Top Seguidores </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-newPost'>
                                    <Link to="funcoes/usuarios-top-seguidores">
                                        <button> Buscar </button>
                                    </Link>
                                </div>
                            </div>
                    </div>
                    <div className="card">

                        <header>
                            <h2> Tweets por Hora </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-newPost'>
                                    <Link to="/funcoes/tweets-por-hora">
                                        <button>Buscar</button>
                                    </Link>
                                </div>
                            </div>
                    </div>
                    <div className="card">

                        <header>
                            <h2> Quantidade Tweets por Idioma</h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-newPost'>
                                    <Link to="funcoes/quantidade-tweets-por-idioma">
                                        <button>Buscar</button>
                                    </Link>
                                </div>
                            </div>
                    </div>
                </div>
            </main>

            
        </div>
    )
}
