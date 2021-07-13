# Boid 계산 최적화
## boid 계산 if문 최소화
Boids.compute에서 분리/정렬/결합을 계산할 물고기 개체수를 계산할 때 if문 최소화<br>
`if(dist > max(max(_SeparateNeighborhoodRadius,_AlignmentNeighborhoodRadius),_CohesionNeighborhoodRadius)) continue;`

Boids 65536개 기준
- 적용 안함(기준): 평균 19fps
- 적용시: 평균 21fps

