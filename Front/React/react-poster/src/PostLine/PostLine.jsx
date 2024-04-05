import './postline.css'
import user from '../assets/user.png'
import { Fragment, useEffect, useState } from 'react'
import ReadButton from '../ReadButton/ReadButton';

export default function PostLine(props){

    const [posts, setPosts] = useState([]);

    useEffect(() =>{
        async function fetchData(){

            let result = await fetch(props.api);

            if(result.ok){

                let jsonPosts = await result.json();

                setPosts(jsonPosts);

                console.log(posts)
            }
        }

        fetchData();

    }, []);

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

    return(
        <div className="posts">
            {
                posts.map(p => (
                    <div key={p.id} className="post">
                        <div className='info-line'>
                            <div className="user-data">
                                <img className='user-img' src={user} alt="" />
                                <span className='user-name'>{p.user.fullName}</span>
                            </div>
                            <span className='date'>
                                {new Date(p.date).toDateString()}
                            </span>
                        </div>
                        <div className="title">
                            {p.title}
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
                        </div>
                    </div>
                ))
            }
        </div>
    )
}