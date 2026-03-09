from flask import Flask, request
app = Flask(__name__)

@app.route('/')
def home():
    return '''
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Jump Race!</title>
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body { font-family: 'Segoe UI', Arial, sans-serif; background: linear-gradient(135deg, #1a1a2e, #16213e, #0f3460); color: white; min-height: 100vh; display: flex; flex-direction: column; align-items: center; justify-content: center; overflow: hidden; }
        h1 { font-size: 2.5rem; margin-bottom: 10px; text-shadow: 0 0 20px rgba(0,200,255,0.5); }
        .subtitle { color: #aaa; margin-bottom: 30px; }
        .name-form { text-align: center; }
        .name-form input[type="text"] { padding: 12px 20px; font-size: 1.1rem; border: 2px solid #0f3460; border-radius: 25px; background: rgba(255,255,255,0.1); color: white; outline: none; width: 250px; transition: border-color 0.3s; }
        .name-form input[type="text"]:focus { border-color: #00d2ff; }
        .name-form button { padding: 12px 30px; font-size: 1.1rem; border: none; border-radius: 25px; background: linear-gradient(90deg, #00d2ff, #3a7bd5); color: white; cursor: pointer; margin-left: 10px; transition: transform 0.2s, box-shadow 0.2s; }
        .name-form button:hover { transform: scale(1.05); box-shadow: 0 0 15px rgba(0,210,255,0.5); }

        #game-area { display: none; width: 90vw; max-width: 800px; text-align: center; }
        .welcome-msg { font-size: 1.3rem; margin-bottom: 15px; }
        .track { position: relative; width: 100%; height: 80px; background: rgba(255,255,255,0.05); border-radius: 10px; margin: 8px 0; border: 1px solid rgba(255,255,255,0.1); overflow: hidden; }
        .runner { position: absolute; left: 5px; top: 50%; transform: translateY(-50%); font-size: 2rem; transition: left 0.3s; }
        .runner.jumping { animation: jump 0.4s ease; }
        @keyframes jump { 0%,100% { transform: translateY(-50%); } 50% { transform: translateY(-100%); } }
        .track-label { position: absolute; left: 10px; top: 5px; font-size: 0.8rem; color: #aaa; }
        .finish-line { position: absolute; right: 40px; top: 0; bottom: 0; width: 3px; background: repeating-linear-gradient(0deg, #fff 0px, #fff 8px, #333 8px, #333 16px); }
        #start-btn, #reset-btn { padding: 12px 35px; font-size: 1.1rem; border: none; border-radius: 25px; cursor: pointer; margin: 15px 5px; transition: transform 0.2s; }
        #start-btn { background: linear-gradient(90deg, #11998e, #38ef7d); color: white; }
        #start-btn:hover { transform: scale(1.05); }
        #reset-btn { background: linear-gradient(90deg, #eb3349, #f45c43); color: white; display: none; }
        #reset-btn:hover { transform: scale(1.05); }
        #result { font-size: 1.5rem; margin-top: 15px; min-height: 40px; text-shadow: 0 0 10px rgba(255,215,0,0.5); }
        .instructions { color: #aaa; font-size: 0.9rem; margin-top: 10px; }
    </style>
</head>
<body>
    <div class="name-form" id="name-form">
        <h1>🏃 Jump Race! 🏃</h1>
        <p class="subtitle">Enter your name to join the race</p>
        <input type="text" id="player-name" placeholder="Your name..." maxlength="15" autofocus>
        <button onclick="startGame()">Let's Race!</button>
    </div>

    <div id="game-area">
        <h1>🏁 Jump Race! 🏁</h1>
        <p class="welcome-msg">Go, <span id="display-name"></span>! Mash the SPACE bar to win!</p>

        <div class="track" id="track-player">
            <span class="track-label" id="track-label-player">You</span>
            <div class="finish-line"></div>
            <span class="runner" id="runner-player">❤️</span>
        </div>
        <div class="track" id="track-cpu1">
            <span class="track-label">Bot Alice</span>
            <div class="finish-line"></div>
            <span class="runner" id="runner-cpu1">🥰</span>
        </div>
        <div class="track" id="track-cpu2">
            <span class="track-label">Bot Bob</span>
            <div class="finish-line"></div>
            <span class="runner" id="runner-cpu2">🙀</span>
        </div>

        <button id="start-btn" onclick="startRace()">Start Race (then mash SPACE!)</button>
        <button id="reset-btn" onclick="resetRace()">Race Again</button>
        <p id="result"></p>
        <p class="instructions">Press SPACE as fast as you can to move your runner!</p>
    </div>

    <script>
        let playerName = '';
        let raceActive = false;
        let playerPos = 5, cpu1Pos = 5, cpu2Pos = 5;
        const finishPos = 88;
        let cpuInterval;

        function startGame() {
            const name = document.getElementById('player-name').value.trim();
            if (!name) { document.getElementById('player-name').focus(); return; }
            playerName = name;
            document.getElementById('display-name').textContent = playerName;
            document.getElementById('track-label-player').textContent = playerName;
            document.getElementById('name-form').style.display = 'none';
            document.getElementById('game-area').style.display = 'block';
        }

        document.getElementById('player-name').addEventListener('keydown', function(e) {
            if (e.key === 'Enter') startGame();
        });

        function startRace() {
            if (raceActive) return;
            raceActive = true;
            playerPos = 5; cpu1Pos = 5; cpu2Pos = 5;
            document.getElementById('result').textContent = '';
            document.getElementById('start-btn').style.display = 'none';
            document.getElementById('reset-btn').style.display = 'none';
            updatePositions();

            cpuInterval = setInterval(() => {
                if (!raceActive) return;
                cpu1Pos += Math.random() * 5;
                cpu2Pos += Math.random() * 4 + 1; // Bob is a bit slower
                triggerJump('runner-cpu1');
                triggerJump('runner-cpu2');
                updatePositions();
                if (cpu1Pos >= finishPos) endRace('Bot Alice');
                else if (cpu2Pos >= finishPos) endRace('Bot Bob');
            }, 200);
        }

        function triggerJump(id) {
            const el = document.getElementById(id);
            el.classList.remove('jumping');
            void el.offsetWidth;
            el.classList.add('jumping');
        }

        document.addEventListener('keydown', function(e) {
            if (e.code === 'Space' && raceActive) {
                e.preventDefault();
                playerPos += 3;
                triggerJump('runner-player');
                updatePositions();
                if (playerPos >= finishPos) endRace(playerName);
            }
        });

        function updatePositions() {
            document.getElementById('runner-player').style.left = Math.min(playerPos, finishPos) + '%';
            document.getElementById('runner-cpu1').style.left = Math.min(cpu1Pos, finishPos) + '%';
            document.getElementById('runner-cpu2').style.left = Math.min(cpu2Pos, finishPos) + '%';
        }

        function endRace(winner) {
            raceActive = false;
            clearInterval(cpuInterval);
            const isPlayer = winner === playerName;
            document.getElementById('result').textContent = isPlayer ? '🎉 ' + winner + ' WINS! You are the champion! 🏆' : '😅 ' + winner + ' wins! Try again!';
            document.getElementById('reset-btn').style.display = 'inline-block';
        }

        function resetRace() {
            playerPos = 5; cpu1Pos = 5; cpu2Pos = 5;
            updatePositions();
            document.getElementById('result').textContent = '';
            document.getElementById('start-btn').style.display = 'inline-block';
            document.getElementById('reset-btn').style.display = 'none';
        }
    </script>
</body>
</html>
'''

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)