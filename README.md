# Tlatonai Development Plan

**Status:** Building Real-Time Multiplayer PvP Game  
**Current Version:** 0.2 Multiplayer Prototype  
**Target Release:** March 2027 (v1.0 Multiplayer)

---

## Executive Summary

Tlatonai is architected as a real-time multiplayer PvP battle game where 2–8 simultaneous players compete in dynamic, skill-based matches. The single-player prototype (surviving 13 AI waves) serves as the gameplay foundation; multiplayer PvP transforms this into a competitive multiplayer architecture with per-player resource economies, real-time unit spawning, and distributed base layouts.

The development roadmap consists of three phases:
1. **Phase 1: Multiplayer Network Foundation** — Establish multiplayer-first client-server architecture and basic multi-player functionality
2. **Phase 2: Multiplayer Content & Balance** — Implement full player-controlled unit combat, strategic buildings, and three map variants
3. **Phase 3: Competitive Polish & Release** — Optimize performance, implement anti-cheat and matchmaking, achieve competitive polish for public launch

---

## Phase 1: Multiplayer Network Foundation

**Goal:** Establish multiplayer-first client-server architecture, refactor core systems for per-player state tracking, and enable real-time synchronization of all player actions and world state in shared game worlds.

**Estimated Duration:** June 2026 – October 2026  
**Checkpoint:** June 30, 2026

### Key Deliverables

#### 1.1 Client-Server Network Transport
- Implement server-authoritative architecture with 60 Hz tick rate
- Establish UDP-based network protocol with framing and reliability layer
- Support client-side prediction and server reconciliation
- Implement delta compression for bandwidth-efficient state sync
- Target: <100ms latency, <5% packet loss recovery
- **Acceptance Criteria:**
  - Network transport handles 8 concurrent clients without packet loss
  - Server tick rate stable at 60 Hz under sustained load
  - Delta compression reduces bandwidth by 70% vs. full state updates
  - Client prediction smooth interpolation with <50ms visible latency

#### 1.2 Per-Player State System
- Refactor resource ledgers (wood, stone, food, gold) as per-player entities
- Implement per-player building ownership and damage tracking
- Create per-player unit registry with command queueing
- Separate player-local input handling from shared world state
- **Acceptance Criteria:**
  - Each player's resources independently tracked and synchronized
  - Building ownership and health synced in real-time
  - Unit commands queue locally and execute on server validation
  - State divergence <100ms under 100ms latency conditions

#### 1.3 Match Manager & Lifecycle
- Implement match setup: room creation, player joining, team assignment
- Create Setup Phase (60s): initial resource allocation, placement window
- Implement Active Combat phase with automatic time limits (10–15 min)
- Victory condition detection: last player standing, base destruction, time limit
- **Acceptance Criteria:**
  - 2–4 player matches launch and complete without crashes
  - Setup Phase enforces 60s timer; players cannot act before Active Combat
  - Victory conditions correctly identify winner (last standing or highest health)
  - Match state persists through client disconnects (reconnect within 30s)

#### 1.4 Player Input & Command System
- Implement per-player input polling and validation at server
- Create unit spawn command validation (resource deduction, building checks)
- Implement building placement command validation
- Add input history logging for anti-cheat and replays
- **Acceptance Criteria:**
  - All player commands validated server-side before execution
  - Resource deduction happens atomically with unit spawn
  - Invalid commands rejected with error feedback to client
  - Input history complete for 100% match replay

#### 1.5 2–4 Player Matchmaking Setup
- Create basic player queue system
- Implement simple skill-based grouping (placeholder for Phase 3)
- Support manual match creation with custom player limits
- **Acceptance Criteria:**
  - Players can create/join matches for 2, 3, or 4 players
  - Matchmaking assigns players to games within 10 seconds of joining queue

### Technical Debt & Refactoring
- Separate single-player wave loop from core gameplay systems
- Remove references to "enemy spawning" in unit system (now player-spawned)
- Refactor building system to support ownership flags and per-player visibility
- Update UI to show per-player resources and unit counts

### Success Checkpoint: June 30, 2026
- Network transport integrated; 2–4 player matches playable end-to-end
- Per-player state synced; <100ms visible latency on LAN
- Setup Phase and Active Combat phases functioning
- Match completion and victory detection working

---

## Phase 2: Multiplayer Content & Balance

**Goal:** Implement full player-controlled unit combat, expand multiplayer-specific buildings with strategic depth, design three map variants optimized for 2/4/8 player matches, and validate competitive balance through extensive playtesting.

**Estimated Duration:** October 2026 – February 2027  
**Checkpoints:** October 15, 2026; December 15, 2026

### Key Deliverables

#### 2.1 Full Player-Controlled Unit Combat
- Implement all four unit types with distinct mechanics and stat profiles:
  - **Archer**: 12 damage, 1.2s attack speed, 300 range, 50 HP
  - **Knight**: 18 damage, 1.8s attack speed, melee (100 range), 120 HP
  - **Scout**: 8 damage, 0.8s attack speed, 250 range, 40 HP, +40% move speed
  - **Mage**: 16 damage (AoE, 150 radius), 2.0s attack speed, 350 range, 60 HP, mana system
- Implement unit-vs-unit combat with damage calculation, death handling
- Create command system: move, attack-move, hold position, retreat waypoints
- Implement formation awareness to prevent unit stacking
- **Acceptance Criteria:**
  - All four unit types spawn and respond to commands
  - Unit combat produces correct damage values and kills
  - Unit AI avoids clumping; formations maintain spacing
  - 50+ simultaneous units execute commands without lag

#### 2.2 Multiplayer-Specific Buildings
- Implement barracks variants with specialized unit types per building
- Create defensive towers with targeting and damage falloff
- Add resource generators (wood camps, stone quarries, farms, markets)
- Implement building upgrades affecting resource production and unit stats
- Building destruction triggers resource refunds (50% return)
- **Acceptance Criteria:**
  - Each building type has unique mechanics and upgrade paths
  - Towers automatically target hostile units within 400 range
  - Resource generators produce resources on schedule; players see tickers
  - Building destruction yields 50% resource refund after 2s

#### 2.3 Three Map Variants
- **Duelist Arena** (2 players):
  - 128×128 grid, symmetric layout, central neutral zone
  - Two bases at opposite corners, 50 tile separation
  - 15 neutral resource nodes scattered symmetrically
  - 10–12 minute match time limit
  
- **Crossroads** (4 players):
  - 192×192 grid, four-way symmetric layout
  - Bases at cardinal positions; 60 tile separation from center
  - Central neutral zone (40×40); contested resource nodes
  - 12–15 minute match time limit
  
- **Valley Siege** (8 players):
  - 256×256 grid, asymmetric terrain with two valleys
  - Bases distributed around perimeter; 80–100 tile distances
  - Chokepoints and strategic high-ground positions
  - Neutral resource nodes clustered in central valley
  - 15+ minute match time limit (prevents early snowball)

- **Acceptance Criteria:**
  - All three maps load without crashes; terrain renders correctly
  - Base placements prevent early rush cheese (minimum 60 tiles separation)
  - Neutral resource distribution encourages map control play
  - All maps tested 10+ times with expected player counts

#### 2.4 Competitive Balance Validation
- Conduct 100+ test matches across all three maps
- Track win rates per unit type, building type, and strategy
- Ensure no single strategy wins >50% of matches
- Collect player feedback on dominance patterns
- Adjust unit costs, damage, and cooldowns based on data
- **Acceptance Criteria:**
  - Win rate for any single unit type or strategy: 25–50%
  - At least three distinct winning strategies validated (infantry rush, boom-economy, mixed)
  - No map has winner bias >60% for any starting position
  - Balance changes logged with version control; 40+ test iterations completed

#### 2.5 8-Player Matchmaking Support
- Extend matchmaking to support Duelist, Crossroads, and Valley Siege maps
- Implement player-count-based match assignment
- Skill-based grouping (Phase 3 polish, basic implementation now)
- **Acceptance Criteria:**
  - Matchmaking correctly assigns players to map variants based on party size
  - 8-player matches launch successfully on Valley Siege

### Success Checkpoint: October 15, 2026
- All four unit types implemented with combat mechanics
- Three map variants created and playtested
- Balance validation begun; 40+ test matches completed

### Success Checkpoint: December 15, 2026
- 100+ competitive test matches completed; balance data collected
- Building system expanded; upgrades functioning
- All maps fully playable with 2/4/8 player matchmaking

---

## Phase 3: Competitive Polish & Release

**Goal:** Optimize performance for 50+ simultaneous units at 60 FPS, implement latency compensation for responsive multiplayer gameplay, establish anti-cheat and skill-based matchmaking systems, and achieve competitive polish for public release.

**Estimated Duration:** December 2026 – March 2027  
**Checkpoints:** December 15, 2026; February 1, 2027; March 2027

### Key Deliverables

#### 3.1 Performance Optimization
- Optimize unit pathfinding for 50+ simultaneous units
- Implement spatial hashing for efficient collision detection
- GPU acceleration for rendering 50+ units with minimal frame drops
- Reduce network bandwidth through improved delta compression
- Implement adaptive quality settings (resolution scaling, effect reduction)
- **Acceptance Criteria:**
  - 50+ simultaneous units at 60 FPS with <5% frame variance
  - Pathfinding 10+ units per frame without noticeable stutter
  - Bandwidth <500 Kbps for 8-player matches at 60 Hz
  - Quality settings maintain 60 FPS on mid-range hardware (GTX 1060, 16GB RAM)

#### 3.2 Latency Compensation
- Implement client-side prediction with extrapolation for unit movement
- Server-side reconciliation with smooth correction for desynchronization
- Interpolation of remotely-controlled units with sub-frame smoothing
- Input buffering to mask network latency (<50ms perceived delay)
- **Acceptance Criteria:**
  - Units move smoothly despite 100ms network latency
  - No "teleporting" or "rubber banding" on reconciliation
  - Player inputs respond within 50ms perceived latency
  - Desync corrections invisible to player (smooth lerp, <200ms)

#### 3.3 Anti-Cheat System
- Server-side validation of all player actions (unit spawns, building placements, attacks)
- Replay recording system: store all match inputs for review
- Anomaly detection for inhuman input patterns (impossible APM, reaction times)
- Flagging system for suspicious matches; manual review by administrators
- **Acceptance Criteria:**
  - All player commands validated server-side; no client-side trust
  - Replays capture complete input history; playback matches live game
  - Anomaly detection flags 95%+ of known cheating patterns
  - <2% false-positive rate for legitimate players

#### 3.4 Skill-Based Matchmaking
- Implement Elo/Glicko rating system tracking per-player skill
- Rating calculated from match outcomes; uncertainty decreases with match count
- Matchmaking targets skill gaps <200 points for balanced games
- Placement matches (10 games) for new players to seed initial rating
- **Acceptance Criteria:**
  - Player ratings converge within 20 matches to stable value
  - Average skill gap in matched games: <200 points
  - Placement matches produce reasonable initial ratings (<15% reassignment after 20 matches)
  - Leaderboard shows top 1000 players globally

#### 3.5 Competitive Polish
- Implement lobby system with team chat and emotes
- Add spectator mode for live match viewing (obs-compatible streaming)
- Implement replay browser (filter by player, date, rating range)
- Add seasonal rankings and reset mechanics
- Implement cosmetic shop (skins, base themes, unit skins)
- **Acceptance Criteria:**
  - Lobby supports 8 players with text/voice chat integration
  - Spectator mode supports 50+ concurrent viewers per match
  - Replays load within 5 seconds; scrubbing is frame-accurate
  - Seasonal reset occurs monthly; rankings reset with soft MMR reset (70% of previous)
  - Cosmetic shop integrates with in-game currency (earned or purchased)

#### 3.6 QA & Stability
- Execute 20+ hour multiplayer stress testing (8 concurrent matches)
- Identify and fix all critical bugs (crashes, data loss, matchmaking failures)
- Performance profiling on target hardware; frame time variance <10%
- Network stability testing under 200ms latency and 5% packet loss
- **Acceptance Criteria:**
  - Zero crashes in 20-hour stress test
  - <5 critical bugs identified in final QA
  - Server uptime 99.9% during QA testing
  - Graceful recovery from network interruptions (reconnect within 30s)

### Success Checkpoint: February 1, 2027
- 100+ competitive matches validated; balance stable
- Anti-cheat system live; zero known exploits
- Matchmaking functional with 100+ active players
- Zero critical bugs in 20+ hour QA sessions
- Performance stable: 60 FPS with 50+ units

### Final Milestone: March 2027
**Tlatonai v1.0 Multiplayer launches** with full PvP support, three map variants, skill-based ranking, and esports-ready competitive infrastructure.

---

## Post-Launch Support (2027–2028)

### Seasonal Updates
- Monthly balance adjustments based on competitive data
- Quarterly content drops (new unit variants, map rotations, cosmetics)
- Ranked season resets every 3 months with leaderboard rewards

### Cross-Platform Multiplayer
- PC (Windows/Linux/Mac) launch (Q2 2027)
- Console support (PS5, Xbox Series X|S) (Q4 2027)
- Mobile (iOS/Android) with cross-platform matchmaking (Q2 2028)
- Maintain competitive fairness across platforms (input method balancing)

### Regional Servers
- North American (US East, US West), European (EU Central, EU West), and Asia-Pacific (APAC) regional servers for <50ms latency

### Clan & Tournament Systems
- Clan management (5–50 member organizations)
- In-game tournament bracket system for competitive events
- Prize pool integration for esports support

### Esports Support
- Ranked ladder with transparent rating system
- Pro esports partnership roadmap (franchised leagues, prize pools)
- Spectator tools and streaming integration

---

## Technical Architecture

### Client-Server Model
- **Server:** Authoritative game state, 60 Hz tick rate, validates all actions
- **Client:** Input handling, prediction, rendering, local UI state
- **Protocol:** UDP with custom reliability and framing; IPv4/IPv6 support
- **Compression:** Delta encoding for state updates; 70% bandwidth savings

### Per-Player State Tracking
- **Resources:** Wood, stone, food, gold (per-player ledgers)
- **Units:** Registry of player-spawned units with ownership
- **Buildings:** Building ownership, health, upgrade status
- **Visibility:** Fog-of-war; players see only their own base and explored map areas

### World State
- **Shared:** Terrain, neutral resource nodes, match timer, winner detection
- **Per-Player:** Base layout, unit positions (visible to owner + shared world)
- **Replicated:** All unit commands, building placements, destruction events

---

## Timeline Overview

```
June 2026                                             March 2027
|====== Phase 1 ======|====== Phase 2 ======|====== Phase 3 ======|
Network Foundation    Content & Balance    Competitive Polish
                     ↑                    ↑
              Oct 15: 40+ tests    Dec 15: 100+ tests
                     ↑                    ↑
              Dec 15: Maps ready    Feb 1: QA Complete

v0.2 Prototype                          v1.0 Multiplayer Release
                                        + Esports-Ready Infrastructure
```

---

## Success Criteria (v1.0)

### Real-Time Multiplayer PvP
- ✓ 2–8 simultaneous players per match
- ✓ Dynamic player-vs-player combat with unit-spawning gameplay
- ✓ 10–15 minute competitive matches
- ✓ Three map variants for different player counts

### Competitive Balance
- ✓ No single unit type or strategy dominates >50% of matches
- ✓ Diverse winning strategies validated across 100+ test matches
- ✓ Win rates for all unit types within 25–50% range
- ✓ No map starting position bias >60%

### Network Performance
- ✓ Sub-100ms latency target; functional up to 200ms
- ✓ 60 FPS with 50+ simultaneous units
- ✓ Client-side prediction with smooth interpolation (<50ms perceived delay)
- ✓ Delta compression: 70% bandwidth reduction vs. full state

### Anti-Cheat & Matchmaking
- ✓ Server-side action validation; no client-side trust
- ✓ Replay recording system with frame-accurate playback
- ✓ Skill-based matchmaking with Elo/Glicko rating system
- ✓ <2% false-positive rate for legitimate players

### Quality
- ✓ <5 critical bugs in final QA
- ✓ Stable 20+ hour multiplayer sessions without crashes
- ✓ 99.9% server uptime during launch week
- ✓ Graceful recovery from network disconnects

---

## Risk Mitigation

| Risk | Likelihood | Impact | Mitigation |
|------|-----------|---------|-----------|
| Netcode desync at scale | Medium | High | Extensive stress testing (100+ concurrent matches), server reconciliation fallbacks |
| Cheating in competitive | Medium | High | Server-side validation, anomaly detection, replay system for review |
| Balance issues post-launch | High | Medium | 100+ playtests pre-launch, seasonal balance updates, community feedback channels |
| Platform fragmentation | Medium | Medium | Cross-platform testing, input method balancing, unified matchmaking |
| Esports sustainability | Low | Medium | Community-driven tournaments, seasonal rankings, prize pool funding roadmap |

---

## Conclusion

Tlatonai's development roadmap prioritizes multiplayer-first architecture from foundation to launch. Phase 1 establishes the technical backbone (network, per-player state, match lifecycle). Phase 2 delivers content diversity and validates competitive balance across 100+ test matches. Phase 3 polishes the experience for public launch, implementing anti-cheat, skill-based matchmaking, and performance optimization.

By March 2027, Tlatonai v1.0 will launch as a fully-featured, esports-ready multiplayer PvP game with 2–8 player support, three map variants, and the competitive infrastructure necessary for seasonal rankings and organized play.

**Target Launch:** March 2027 (v1.0)  
**Concurrent Player Support:** 2–8 per match; 1000+ total online players  
**Regional Infrastructure:** NA, EU, APAC servers with <50ms latency targets
