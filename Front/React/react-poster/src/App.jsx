import { Fragment, useState } from 'react'
import './App.css'
import MainPart from './MainPart/MainPart'
import Footer from './Footer/Footer'
import Header from './Header/Header'
import Button from './Button/Button'
import PostLine from './PostLine/PostLine'
import PublicationPage from './Publication/PublicationPage'
import UserPage from './User/UserPage'

function App() {

  const [active, setActive] = useState("third");

  function handleClick(btnNumber){
    setActive(btnNumber);
  }

  return (
    <Fragment>
      <Header>
        <Button isActive={"first"===active} onClick={() => handleClick("first")} mr={20} >
          смотреть статьи
        </Button>
        <Button isActive={"second"===active} onClick={() => handleClick("second")} mr={20}>
          опубликовать пост
        </Button>
        <Button isActive={"third"===active} onClick={() => handleClick("third")}>
          учетная запись
        </Button>
      </Header>
      <MainPart>
        {
          active === 'first' ? <PostLine api="http://localhost:8080/api/post/all"></PostLine> : null
        }
        {
          active === 'second' ? <PublicationPage></PublicationPage> : null
        }
        {
          active === 'third' ? <UserPage></UserPage> : null
        }
      </MainPart>
      <Footer>

      </Footer>
    </Fragment>
  )
}

export default App
