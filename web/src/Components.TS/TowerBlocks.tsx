import React, { useEffect } from 'react';
import './TowerBlocks.css';
import './TowerBlocks.ts';

const TowerBlocksGame = () => {
  useEffect(() => {
    // Load external scripts needed for the game
    const loadScript = (src: string, id: string): Promise<void> => {
      return new Promise((resolve) => {
        // Skip if script is already loaded
        if (document.getElementById(id)) {
          resolve();
          return;
        }
        
        const script = document.createElement('script');
        script.id = id;
        script.src = src;
        script.async = true;
        script.onload = () => resolve();
        document.body.appendChild(script);
      });
    };

    // Load scripts in sequence
    const loadScripts = async () => {
      await loadScript('https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js', 'three-js');
      await loadScript('https://cdnjs.cloudflare.com/ajax/libs/gsap/3.9.1/gsap.min.js', 'gsap');
      
      // Only after all scripts are loaded, load our game script
      const gameScript = document.createElement('script');
      gameScript.src = './Components.TS/TowerBlocks.js'; // This will be processed by Vite
      document.body.appendChild(gameScript);
    };

    loadScripts();
    
    // Cleanup function
    return () => {
      // Remove game-specific elements if needed
      const gameScript = document.querySelector('script[src="./TowerBlocks.ts"]');
      if (gameScript) document.body.removeChild(gameScript);
    };
  }, []);

  return (
    <div id="container">
        <div id="game"></div>
        <div id="score">0</div>
        <div id="instructions">Click (or press the spacebar) to place the block</div>
        <div className="game-over">
            <h2>Game Over</h2>
            <p>You did great, you're the best.</p>
            <p>Click or spacebar to start again</p>
        </div>
        <div className="game-ready">
            <div id="start-button">Start</div>
            <div></div>
        </div>
    </div>

  );
};

export default TowerBlocksGame;