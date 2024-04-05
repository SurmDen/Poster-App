import { useEffect, useState } from 'react'
import './publication.css'

export default function PublicationPage(props){

    const [categories, setCategories] = useState([]);

    async function fetchCategoriesAsync(){

        const response = await fetch("http://localhost:8080/api/category/all");

        if(response.ok){

            let jsonData = await response.json();

            console.log(jsonData);

            setCategories(jsonData);
        }
    }

    useEffect(()=>{
        fetchCategoriesAsync()
    }, []);


    const [postData, setDataToPost] = useState(
        {
            title:'',
            introdution:'', 
            mainPart:'',
            conclusion:'',
            userId:localStorage.getItem('id'),
            postCategoryId:''
        });


    function handlePostFormChange(e){

        const data = e.target.value;

        const name = e.target.name;

        if(name === 'category'){
            setDataToPost(prev =>({
                title:prev.title,
                introdution:prev.introdution, 
                mainPart:prev.mainPart,
                conclusion:prev.conclusion,
                userId:prev.userId,
                postCategoryId:data
            })); 
        }
        else if(name === 'title'){
            setDataToPost(prev =>({
                title:data,
                introdution:prev.introdution, 
                mainPart:prev.mainPart,
                conclusion:prev.conclusion,
                userId:prev.userId,
                postCategoryId:prev.postCategoryId
            })); 
        }
        else if(name === 'intro'){
            setDataToPost(prev =>({
                title:prev.title,
                introdution:data, 
                mainPart:prev.mainPart,
                conclusion:prev.conclusion,
                userId:prev.userId,
                postCategoryId:prev.postCategoryId
            })); 
        }
        else if(name === 'main'){
            setDataToPost(prev =>({
                title:prev.title,
                introdution:prev.introdution, 
                mainPart:data,
                conclusion:prev.conclusion,
                userId:prev.userId,
                postCategoryId:prev.postCategoryId
            })); 
        }
        else if(name === 'concl'){
            setDataToPost(prev =>({
                title:prev.title,
                introdution:prev.introdution, 
                mainPart:prev.mainPart,
                conclusion:data,
                userId:prev.userId,
                postCategoryId:prev.postCategoryId
            })); 
        }

    }

    async function sendPostDataAsync(e){

        e.preventDefault(false);

        const response = await fetch("http://localhost:8080/api/post/create",{
            method:'POST',
            headers:{
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            },
            body:JSON.stringify(postData)
        });

        if(response.ok){
            console.log("success post creating");
        }
    }


    return(
        <div className='pub-container'>
            <h1>ПУБЛИКАЦИЯ СТАТЬИ</h1>
            <form>
                <div className="pub-group">
                    <span className='section-title'>Выберите категорию</span>
                    <select onChange={(e)=>handlePostFormChange(e)} value={postData.postCategoryId} name='category'>
                        {
                            categories.map(cat => (
                                <option key={cat.id} value={cat.id}>{cat.categoryName}</option>
                            ))
                        }
                    </select>
                </div>
                <div className="pub-group">
                    <span className='section-title'>Название поста</span>
                    <textarea onChange={(e)=>handlePostFormChange(e)} name='title' value={postData.title} style={{maxHeight:50, minHeight:50, fontSize:22}}>
                        
                    </textarea>
                </div>
                <div className="pub-group">
                    <span className='section-title'>Введение</span>
                    <textarea onChange={(e)=>handlePostFormChange(e)} name='intro' value={postData.introdution} style={{minHeight:250}}>
                        
                    </textarea>
                </div>
                <div className="pub-group">
                    <span className='section-title'>Основная часть</span>
                    <textarea onChange={(e)=>handlePostFormChange(e)} name='main' value={postData.mainPart} style={{minHeight:250}}>
                        
                    </textarea>
                </div>
                <div className="pub-group">
                    <span className='section-title'>Заключение</span>
                    <textarea onChange={(e)=>handlePostFormChange(e)} name='concl' value={postData.conclusion} style={{minHeight:250}}>
                        
                    </textarea>
                </div>
                <button onClick={(e) => sendPostDataAsync(e)} className='pub-button'>опубликовать</button>
            </form>
        </div>
    )
}