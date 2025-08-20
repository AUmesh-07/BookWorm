import React from 'react'
import HeroSection from '../components/HeroSection'
import Categories from '../components/Categories'
import FeaturedBooks from '../components/FeaturedBooks'
import PopularAudiobooks from '../components/PopularAudiobooks'
import TrendingMusic from '../components/TrendingMusic'
import PremiumSection from '../components/PremiumSection'
import Footer from '../components/Footer'

function HomePage1() {
  return (
    <div>
        <main> 
      <HeroSection/>
      <Categories/>
      <FeaturedBooks/>
      <PremiumSection/>
      {/* <PopularAudiobooks/> */}
      {/* <TrendingMusic/> */}
        </main>
      <Footer/>
    </div>
  )
}

export default HomePage1
