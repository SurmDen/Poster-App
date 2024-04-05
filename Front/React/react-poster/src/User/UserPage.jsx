import { useEffect, useState, Fragment} from 'react'
import PostButton from './PostButton'
import SwitchButton from './SwitchButton'
import './user.css'
import '../PostLine/postline.css'
import user from '../assets/account.png'
import ReadButton from '../ReadButton/ReadButton'
import busket from '../assets/basket.png'

export default function UserPage(){

    const [currentUser, setCurrentUser] = useState({});

    const [posts, setPosts] = useState([]);

    async function getCurrentUser(){

        const token = localStorage.getItem('token');

        const currentUserId = localStorage.getItem('id');

        const response = await fetch(`http://localhost:8080/api/user/${currentUserId}`, {
            method:"GET",
            headers:{
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${token}`
            }
        });

        if(response.ok){

            setBlock('user');

            const jsonCurrentUser = await response.json();

            console.log(jsonCurrentUser);

            setCurrentUser(jsonCurrentUser);

            setPosts(jsonCurrentUser.posts)
        }
    }

    useEffect(()=>{
        getCurrentUser();
    }, [])

    const createAccountUrl = "http://localhost:8080/api/user/create";

    const loginUrl = "http://localhost:8080/api/user/signin";

    async function createUserAsync(e){

        e.preventDefault(false);

        const user = {
            fullname: createForm.name,
            password: createForm.pass,
            email: createForm.email
        }

        let response = await fetch(createAccountUrl, {

            method:"POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body:JSON.stringify(user)
        });

        if(response.ok){
            const jsonResponse = await response.json();

            localStorage.setItem('token', jsonResponse.token);

            localStorage.setItem('id', jsonResponse.userId);

            getCurrentUser();

        }
    }

    async function loginUserAsync(e){

        e.preventDefault(false);

        const user = {
            username: loginForm.name,
            password: loginForm.pass
        }

        console.log(JSON.stringify(user));

        let response = await fetch(loginUrl, {

            method:"POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body:JSON.stringify(user)
        });

        if(response.ok){

            const jsonResponse = await response.json();

            localStorage.setItem('token', jsonResponse.token);

            localStorage.setItem('id', jsonResponse.userId);

            getCurrentUser();
        }
    }


    const[signForm, setSignForm] = useState("login");

    const [createForm, setCreateForm] = useState({name:"имя", email:"эл. почта", pass:"пароль"});

    const [loginForm, setLoginForm] = useState({name:"имя", pass:"пароль"});

    const [block, setBlock] = useState("sign");

    function handleSwitchButton(value){

        setSignForm(value);
    }

    function handleCreateInput(e){

        const value = e.target.value;

        if(e.target.name === 'name'){
            setCreateForm(prev =>({name:value,email: prev.email, pass:prev.pass}));
        }

        if(e.target.name === 'email'){
            setCreateForm(prev =>({name:prev.name,email:value, pass:prev.pass}));
        }

        if(e.target.name === 'pass'){
            setCreateForm(prev =>({name:prev.name,email: prev.email, pass:value}));

            e.target.type = 'password';
        }

    }

    function handleLoginInput(e){

        const value = e.target.value;

        if(e.target.name === 'name'){
            setLoginForm(prev =>({name:value, pass:prev.pass}));
        }

        if(e.target.name === 'pass'){
            setLoginForm(prev =>({name:prev.name, pass:value}));

            e.target.type = 'password';
        }

    }


    const [read, setRead] = useState(0);

    function handleReadButton( number){
        setRead((prev)=>{
            if(number === prev){
                return 0;
            }
            else{
                return number;
            }
        });
    }

    function handleExitButton(){
        localStorage.removeItem('token');
        localStorage.removeItem('id');
        setBlock('sign');
    }

    async function removePostAsync(id){
        const response = await fetch(`http://localhost:8080/api/post/delete`, {
            method:"DELETE",
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            },
            body:id
        });

        if(response.ok){
            getCurrentUser();
        }

    }

    return(
        <div className='user-container'>
            {
                block === 'sign' ? <div className="signing-block">
                <div className="switch-line">
                    <SwitchButton onClick={() => handleSwitchButton('login')} isActive={signForm === 'login'}>
                        Войти
                    </SwitchButton>
                    <SwitchButton onClick={() => handleSwitchButton('create')} isActive={signForm === 'create'}>
                        Создать
                    </SwitchButton>
                </div>
                {
                    signForm === 'create' ? <div className="create-acc">
                    <form>
                        <input name='name' onChange={(e) => handleCreateInput(e)} type="text" value={createForm.name}/>
                        <input name='email' onChange={(e) => handleCreateInput(e)} type="text" value={createForm.email}/>
                        <input name='pass' onChange={(e) => handleCreateInput(e)} type="text" value={createForm.pass}/>
                        <PostButton onClick={(e) => createUserAsync(e)}>
                            регистрация
                        </PostButton>
                    </form>
                    </div> : null
                }
                {
                    signForm === 'login' ? <div className="login">
                    <form>
                        <input type="text" name='name' value={loginForm.name} onChange={(e) => handleLoginInput(e)}/>
                        <input type="text" name='pass' value={loginForm.pass} onChange={(e) => handleLoginInput(e)}/>
                        <PostButton onClick={(e) => loginUserAsync(e)}>
                            вход
                        </PostButton>
                    </form>
                    </div> : null
                }
                </div> : null
            }
            {
                block === 'user' ? 
                <div className="user-block">
                    <button onClick={() => handleExitButton()} className='exit-button'>ВЫХОД</button>
                    <div className="user-block-info">
                            <img src={user} alt="" />
                            <div className="user-block-name">{currentUser.fullName}</div>
                            <div className="user-block-email">({currentUser.email})</div>
                            <h1>Мои статьи</h1>
                            <div className="posts">
                            {
                                posts.map(p => (
                                    <div key={p.id} className="post">
                                        <div className='info-line'>
                                            <div className="title">
                                                {p.title}
                                            </div>
                                            <span className='date'>
                                                {new Date(p.date).toDateString()}
                                            </span>
                                        </div>
                                        <div className="preview">
                                            <p>
                                                {p.introdution}
                                            </p>
                                            {
                                                read === p.id ? <Fragment><p>{p.mainPart}</p><p>{p.conclusion}</p></Fragment> : ""
                                            }
                                        </div>
                                        <div className="full">
                                            <ReadButton isActive={read===p.id} onClick={() => handleReadButton(p.id)}></ReadButton>
                                            <button onClick={() => removePostAsync(p.id)} className='remove-btn'><img src={busket} alt="" /></button>
                                        </div>
                                    </div>
                                ))
                            }
                            </div>
                    </div>
                </div> : null
            }
        </div>
    )
}